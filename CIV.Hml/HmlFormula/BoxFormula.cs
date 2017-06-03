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
    }
}
