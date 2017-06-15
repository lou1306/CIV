using System;
using System.Collections.Generic;
using CIV.Common;
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

        IDictionary<string, CcsProcess> _pool = new Dictionary<string, CcsProcess>();

        /// <summary>
        /// Create a Process from the specified context.
        /// </summary>
        /// <returns>The create.</returns>
        /// <param name="context">Context.</param>
        public CcsProcess Create(ProcessContext context)
        {

            if (_pool.ContainsKey(context.GetText()))
            {
                return _pool[context.GetText()];
            }
            CcsProcess result;
            switch(context)
            {
                case NilProcContext c:
                    return NilProcess.Instance;
                case PrefixProcContext c:
					result = new PrefixProcess
					{
						Label = c.label().GetText(),
						Inner = new CcsProxy(this, c.process())
					};
                    break;
                case PidProcContext c:
					var pid = c.pid().GetText();
                    result = Create(NamedProcessesTable[pid]);
                    result.Pid = pid;
                    break;
                case ParenthProcContext c:
					result = Create(c.process());
					break;
				case ParProcContext c:
					result = new ParProcess
					{
						Inner1 = new CcsProxy(this, c.process(0)),
						Inner2 = new CcsProxy(this, c.process(1))
					};
					break;
				case ChoiceProcContext c:
					result = new ChoiceProcess
					{
						Inner1 = new CcsProxy(this, c.process(0)),
						Inner2 = new CcsProxy(this, c.process(1))
					};
					break;
				case RestrictIdProcContext c:
					result = new RestrictedProcess
					{
						Inner = new CcsProxy(this, c.process()),
						Restrictions = NamedSetsTable[c.setId().GetText()]
					};
					break;
				case RestrictExprProcContext c:
					result = new RestrictedProcess
					{
						Inner = new CcsProxy(this, c.process()),
						Restrictions = InlineSetsTable[c.setExpression()]
					};
					break;
				case RelabelProcContext c:
					result = new RelabeledProcess
					{
						Inner = new CcsProxy(this, c.process()),
						Relabeling = Relabelings[c.relabelExpression()]
					};
					break;
				default:
                    throw new NotSupportedException();
			}
            _pool[context.GetText()] = result;
            return result;
        }
    }
}
