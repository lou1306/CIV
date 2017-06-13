using System;
using System.Collections.Generic;
using System.Linq;
using CIV.Common;

namespace CIV.Hml
{
    class DiamondFormula : HmlLabelFormula
    {
        protected override bool CheckStrategy(IEnumerable<IProcess> processes)
            => processes.Any(Inner.Check);

        protected override IEnumerable<Transition> TransitionStrategy(IProcess process)
        {
            return process.GetTransitions();
        }
        protected override string BuildRepr()
        {
            if (!(Label is TopSet<string>))
                return $"<{String.Join(",", Label)}>{Inner}";
            else
                return $"<->{Inner}";
        }

		public override bool Check(IProcess process)
		{
			var grouped = MatchedTransitions(process).GroupBy(x => x.Label);
            return grouped.Any(procs => procs.Any(p => Inner.Check(p.Process)));
		}
    }
}
