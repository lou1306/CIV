using System;
using System.Collections.Generic;
using CIV.Ccs;

namespace CIV
{
    public class CcsListener : CcsParserBaseListener
    {
        public IDictionary<String, CcsParser.ProcessContext> Processes { get; set; }
        public IDictionary<String, ISet<String>> NamedSets { get; set; }
        public IDictionary<CcsParser.SetExpressionContext, ISet<String>> ExprSets { get; set; }
        public IDictionary<CcsParser.RenamingExpressionContext, IDictionary<String, String>> Renamings { get; set; }

        Stack<ISet<String>> setsStack;
        Stack<IDictionary<String, String>> renamingsStack;

        public CcsListener()
        {
            Processes = new Dictionary<String, CcsParser.ProcessContext>();
            NamedSets = new Dictionary<String, ISet<String>>();
            ExprSets = new Dictionary<CcsParser.SetExpressionContext, ISet<String>>();
            Renamings = new Dictionary<CcsParser.RenamingExpressionContext, IDictionary<String, String>>();
            setsStack = new Stack<ISet<string>>();
            renamingsStack = new Stack<IDictionary<string, string>>();
        }

        public Processes.ProcessFactory GetProcessFactory() =>
                        new Processes.ProcessFactory
                        {
                            NamedProcessesTable = Processes,
                            NamedSetsTable = NamedSets,
                            InlineSetsTable = ExprSets,
                            Renamings = Renamings,
                        };

        //public override void EnterSetDef(CcsParser.SetDefContext context)
        //{
        //    base.EnterSetDef(context);
        //}
        public override void EnterSetList(CcsParser.SetListContext context)
        {
            setsStack.Peek().Add(context.nonTauAction().GetText());
            base.EnterSetList(context);
        }
        public override void EnterSetExpression(CcsParser.SetExpressionContext context)
        {
            setsStack.Push(new HashSet<String>());
        }
        public override void ExitSetExpression(CcsParser.SetExpressionContext context)
        {
            // If we are a SetDef, we will add the set to the NamedSets
            if (context.Parent.GetType() != typeof(CcsParser.SetDefContext))
            {
                ExprSets.Add(context, setsStack.Pop());
            }
            base.ExitSetExpression(context);
        }
        public override void ExitSetDef(CcsParser.SetDefContext context)
        {
            NamedSets.Add(context.IDENTIFIER().GetText(), setsStack.Pop());
            base.ExitSetDef(context);
        }

        public override void EnterRenamingExpression(CcsParser.RenamingExpressionContext context)
        {
            renamingsStack.Push(new Dictionary<String, String>());
        }

        public override void ExitRenamingExpression(CcsParser.RenamingExpressionContext context)
        {
            Renamings.Add(context, renamingsStack.Pop());
            base.ExitRenamingExpression(context);
        }

        public override void EnterRenamingList(CcsParser.RenamingListContext context)
        {
            var renaming = context.renaming();
            renamingsStack.Peek().Add(
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
