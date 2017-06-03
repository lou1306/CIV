using System;
using System.Collections.Generic;
using System.Linq;
using CIV.Ccs;
using CIV.Interfaces;

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
        protected override string BuildRepr() => $"<{String.Join(",", Label)}>{Inner}";
    }
}
