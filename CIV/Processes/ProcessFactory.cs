using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using CIV.Ccs;
using CIV.Helpers;

namespace CIV.Processes
{
    /// <summary>
    /// Process factory that recursively builds Processes from contexts.
    /// </summary>
    public class ProcessFactory
    {
        public IDictionary<String, CcsParser.ProcessContext> NamedProcessesTable { get; set; }
        public IDictionary<String, ISet<String>> NamedSetsTable { get; set; }
        public IDictionary<CcsParser.SetExpressionContext, ISet<String>> InlineSetsTable { get; set; }
        public IDictionary<CcsParser.RelabelExpressionContext, RelabelingFunction> Relabelings { get; set; }


        /// <summary>
        /// Create a Process from the specified context.
        /// </summary>
        /// <returns>The create.</returns>
        /// <param name="context">Context.</param>
        public IProcess Create(ParserRuleContext context)
        {
            //throw new NotSupportedException(context.GetText());
            return Create(context as dynamic);
        }

        public IProcess Create(CcsParser.PrefixProcContext context)
        {
            return new PrefixProcess
            {
                Label = context.label().GetText(),
                Inner = new ProcessProxy(this, context.process())
            };
        }

        IProcess Create(CcsParser.PidProcContext context)
        {
            var pid = context.pid().GetText();
            return new PidProcess(this, NamedProcessesTable[pid])
            {
                Pid = pid
            };
        }

        IProcess Create(CcsParser.NilProcContext context)
        {
            return new NilProcess();
        }

        IProcess Create(CcsParser.ParenthProcContext context)
        {
            return Create(context.process());
        }

        IProcess Create(CcsParser.ParProcContext context)
        {
            return new ParProcess
            {
                Inner1 = new ProcessProxy(this, context.process(0)),
                Inner2 = new ProcessProxy(this, context.process(1))
            };
        }

        IProcess Create(CcsParser.ChoiceProcContext context)
        {
            return new ChoiceProcess
            {
                Inner1 = new ProcessProxy(this, context.process(0)),
                Inner2 = new ProcessProxy(this, context.process(1))
            };
        }

        IProcess Create(CcsParser.RestrictIdProcContext context)
        {
            return new RestrictedProcess
            {
                Inner = new ProcessProxy(this, context.process()),
                Restrictions = NamedSetsTable[context.setId().GetText()]
            };
        }

        IProcess Create(CcsParser.RestrictExprProcContext context)
        {
            return new RestrictedProcess
            {
                Inner = new ProcessProxy(this, context.process()),
                Restrictions = InlineSetsTable[context.setExpression()]
            };
        }

        IProcess Create(CcsParser.RelabelProcContext context)
        {
            return new RelabeledProcess
            {
                Inner = new ProcessProxy(this, context.process()),
                Relabeling = Relabelings[context.relabelExpression()]
            };
        }
    }
}
