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
            switch(context)
            {
                case CcsParser.NilProcContext c:
                    return new NilProcess();
                case CcsParser.PrefixProcContext c:
					return new PrefixProcess
					{
						Label = c.label().GetText(),
						Inner = new ProcessProxy(this, c.process())
					};
                case CcsParser.PidProcContext c:
					var pid = c.pid().GetText();
					return new PidProcess(this, NamedProcessesTable[pid])
					{
						Pid = pid
					};
                case CcsParser.ParenthProcContext c:
					return new ProcessProxy(this, c.process());
                case CcsParser.ParProcContext c:
					return new ParProcess
					{
						Inner1 = new ProcessProxy(this, c.process(0)),
						Inner2 = new ProcessProxy(this, c.process(1))
					};
                case CcsParser.ChoiceProcContext c:
					return new ChoiceProcess
					{
						Inner1 = new ProcessProxy(this, c.process(0)),
						Inner2 = new ProcessProxy(this, c.process(1))
					};
                case CcsParser.RestrictIdProcContext c:
					return new RestrictedProcess
					{
						Inner = new ProcessProxy(this, c.process()),
						Restrictions = NamedSetsTable[c.setId().GetText()]
					};
				case CcsParser.RestrictExprProcContext c:
					return new RestrictedProcess
					{
						Inner = new ProcessProxy(this, c.process()),
						Restrictions = InlineSetsTable[c.setExpression()]
					};
                case CcsParser.RelabelProcContext c:
					return new RelabeledProcess
					{
						Inner = new ProcessProxy(this, c.process()),
						Relabeling = Relabelings[c.relabelExpression()]
					};
                default:
					Console.WriteLine("------------------");
					Console.WriteLine(context);
                    Console.WriteLine(context.GetType());
                    throw new NotSupportedException();
			}
        }
    }
}
