using System;
using System.Linq;
using System.Collections.Generic;
using CIV.Ccs;

namespace CIV.Ccs
{
    public class CcsListener : CcsParserBaseListener
    {
        public IDictionary<String, CcsParser.ProcessContext> Processes { get; set; }
        public IDictionary<String, ISet<String>> NamedSets { get; set; }
        public IDictionary<CcsParser.SetExpressionContext, ISet<String>> ExprSets { get; set; }
        public IDictionary<CcsParser.RelabelExpressionContext, RelabelingFunction> Relabelings { get; set; }


        RestrictionSet currentSet;
        RelabelingFunction currentRenaming;

        public CcsListener()
        {
            Processes = new Dictionary<String, CcsParser.ProcessContext>();
            NamedSets = new Dictionary<String, ISet<String>>();
            ExprSets = new Dictionary<CcsParser.SetExpressionContext, ISet<String>>();
            Relabelings = new Dictionary<CcsParser.RelabelExpressionContext, RelabelingFunction>();
        }

        public IDictionary<string, CcsProcess> GetProcessesTable()
        {
            var factory = new CcsFactory
            {
                NamedProcessesTable = Processes,
                NamedSetsTable = NamedSets,
                InlineSetsTable = ExprSets,
                Relabelings = Relabelings
            };
            return Processes.ToDictionary(
                x => x.Key,
                x => factory.Create(x.Value));
        }
        //new Processes.ProcessFactory
        //{
        //    NamedProcessesTable = Processes,
        //    NamedSetsTable = NamedSets,
        //    InlineSetsTable = ExprSets,
        //    Relabelings = Relabelings
        //};

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

        public override void EnterRelabelExpression(CcsParser.RelabelExpressionContext context)
        {
            currentRenaming = new RelabelingFunction();
        }

        public override void ExitRelabelExpression(CcsParser.RelabelExpressionContext context)
        {
            Relabelings.Add(context, currentRenaming);
        }

        public override void EnterRelabelList(CcsParser.RelabelListContext context)
        {
            var renaming = context.relabel();
            currentRenaming.Add(
                renaming.nonTauAction().GetText(),
                renaming.action().GetText());
        }

        public override void ExitProcDef(CcsParser.ProcDefContext context)
        {
            Processes[context.IDENTIFIER().GetText()] = context.process();
            base.ExitProcDef(context);
        }
    }
}
