using System;
using System.Collections.Generic;
using CIV.Interfaces;
using static CIV.Ccs.CcsParser;

namespace CIV.Ccs
{
    /// <summary>
    /// Process factory that builds Processes from contexts.
    /// </summary>
    class CcsFactory : IFactory<CcsProcess, ProcessContext>
    {
        public IDictionary<String, ProcessContext> NamedProcessesTable { get; set; }
        public IDictionary<String, ISet<String>> NamedSetsTable { get; set; }
        public IDictionary<SetExpressionContext, ISet<String>> InlineSetsTable { get; set; }
        public IDictionary<RelabelExpressionContext, RelabelingFunction> Relabelings { get; set; }

        /// <summary>
        /// Create a Process from the specified context.
        /// </summary>
        /// <returns>The create.</returns>
        /// <param name="context">Context.</param>
        public CcsProcess Create(ProcessContext context)
        {
            switch(context)
            {
                case NilProcContext c:
                    return NilProcess.Instance;
                case PrefixProcContext c:
					return new PrefixProcess
					{
						Label = c.label().GetText(),
						Inner = new CcsProxy(this, c.process())
					};
                case PidProcContext c:
					var pid = c.pid().GetText();
                    return Create(NamedProcessesTable[pid]);
                case ParenthProcContext c:
					return Create(c.process());
                case ParProcContext c:
					return new ParProcess
					{
						Inner1 = new CcsProxy(this, c.process(0)),
						Inner2 = new CcsProxy(this, c.process(1))
					};
                case ChoiceProcContext c:
					return new ChoiceProcess
					{
						Inner1 = new CcsProxy(this, c.process(0)),
						Inner2 = new CcsProxy(this, c.process(1))
					};
                case RestrictIdProcContext c:
					return new RestrictedProcess
					{
						Inner = new CcsProxy(this, c.process()),
						Restrictions = NamedSetsTable[c.setId().GetText()]
					};
				case RestrictExprProcContext c:
					return new RestrictedProcess
					{
						Inner = new CcsProxy(this, c.process()),
						Restrictions = InlineSetsTable[c.setExpression()]
					};
                case RelabelProcContext c:
					return new RelabeledProcess
					{
						Inner = new CcsProxy(this, c.process()),
						Relabeling = Relabelings[c.relabelExpression()]
					};
                default:
                    throw new NotSupportedException();
			}
        }
    }
}
