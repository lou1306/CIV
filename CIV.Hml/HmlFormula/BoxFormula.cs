using System;
using System.Collections.Generic;
using System.Linq;
using CIV.Common;

namespace CIV.Hml
{
    class BoxFormula : HmlLabelFormula
    {
		protected override string BuildRepr()
		{
			if (!(Label is TopSet<string>))
            {
				return $"[{String.Join(",", Label)}]{Inner}";
			}
				return $"[-]{Inner}";
		}
        protected override bool CheckStrategy(IEnumerable<IProcess> processes)
			=> processes.All(Inner.Check);

        protected override IEnumerable<Transition> TransitionStrategy(IProcess process)
        {
			return process.GetTransitions();
		}
		public override bool Check(IProcess process)
		{
            var grouped = MatchedTransitions(process).GroupBy(x => x.Label);
			return grouped.All(procs => procs.All(p => Inner.Check(p.Process)));
		}

        public override IEnumerable<IProcess> O(IEnumerable<IProcess> current, IEnumerable<IProcess> all)
        {
            return all.Where(x => TransitionStrategy(x).All(t => current.Contains(t.Process)));
        }
    }
}
