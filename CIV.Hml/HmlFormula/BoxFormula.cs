using System;
using System.Collections.Generic;
using System.Linq;
using CIV.Ccs;
using CIV.Interfaces;

namespace CIV.Hml
{
    class BoxFormula : HmlLabelFormula
    {
        protected override string BuildRepr() => $"[{String.Join(",", Label)}]{Inner}";

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
    }
}
