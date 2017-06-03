using System;
using System.Collections.Generic;
using System.Linq;
using CIV.Ccs;
using CIV.Interfaces;

namespace CIV.Hml
{
    class WeakBoxFormula : HmlLabelFormula
    {
        protected override string BuildRepr() => $"[[{String.Join(",", Label)}]]{Inner}";

        protected override bool CheckStrategy(IEnumerable<IProcess> processes)
            => processes.All(Inner.Check);

        protected override IEnumerable<Transition> TransitionStrategy(IProcess process)
        {
            if (process is IHasWeakTransitions)
            {
                return ((IHasWeakTransitions)process).GetWeakTransitions();
            }
            throw new ArgumentException();
        }
    }
}
