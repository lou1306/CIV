using System;
using System.Collections.Generic;
using CIV.Ccs;
using CIV.Helpers;

namespace CIV
{
    public class CcsListener : CcsParserBaseListener
    {
        public IDictionary<String, CcsParser.ProcessContext> Processes { get; set; }
        public IDictionary<String, ISet<String>> NamedSets { get; set; }
        public IDictionary<CcsParser.SetExpressionContext, ISet<String>> ExprSets { get; set; }
        public IDictionary<CcsParser.RenamingExpressionContext, RelabelingFunction> Renamings { get; set; }


        RestrictionSet currentSet;
        RelabelingFunction currentRenaming;

        public CcsListener()
        {
            Processes = new Dictionary<String, CcsParser.ProcessContext>();
            NamedSets = new Dictionary<String, ISet<String>>();
            ExprSets = new Dictionary<CcsParser.SetExpressionContext, ISet<String>>();
            Renamings = new Dictionary<CcsParser.RenamingExpressionContext, RelabelingFunction>();
        }

        public Processes.ProcessFactory GetProcessFactory() =>
                        new Processes.ProcessFactory
                        {
                            NamedProcessesTable = Processes,
                            NamedSetsTable = NamedSets,
                            InlineSetsTable = ExprSets,
                            Renamings = Renamings
                        };

        //public override void EnterSetDef(CcsParser.SetDefContext context)
        //{
        //    base.EnterSetDef(context);
        //}
        public override void EnterSetList(CcsParser.SetListContext context)
        {
            currentSet.Add(context.nonTauAction().GetText());
            base.EnterSetList(context);
        }
        public override void EnterSetExpression(CcsParser.SetExpressionContext context)
        {
            currentSet = new RestrictionSet();
        }
        public override void ExitSetExpression(CcsParser.SetExpressionContext context)
        {
            // If we are in a SetDef, we will add the set to NamedSets instead
            if (context.Parent.GetType() != typeof(CcsParser.SetDefContext))
            {
                ExprSets.Add(context, currentSet);
            }
            base.ExitSetExpression(context);
        }
        public override void ExitSetDef(CcsParser.SetDefContext context)
        {
            NamedSets.Add(context.IDENTIFIER().GetText(), currentSet);
            base.ExitSetDef(context);
        }

        public override void EnterRenamingExpression(CcsParser.RenamingExpressionContext context)
        {
            currentRenaming = new RelabelingFunction();
        }

        public override void ExitRenamingExpression(CcsParser.RenamingExpressionContext context)
        {
            Renamings.Add(context, currentRenaming);
            base.ExitRenamingExpression(context);
        }

        public override void EnterRenamingList(CcsParser.RenamingListContext context)
        {
            var renaming = context.renaming();
            currentRenaming.Add(
                renaming.nonTauAction().GetText(),
                renaming.action().GetText());
            base.EnterRenamingList(context);
        }


        public override void ExitProcDef(CcsParser.ProcDefContext context)
        {
            Processes[context.IDENTIFIER().GetText()] = context.process();
            base.ExitProcDef(context);
        }
    }
}
