using System;
using System.Collections.Generic;
using System.Linq;
using CIV.Common;

namespace CIV.Hml
{
    class WeakBoxFormula : BoxFormula
    {
        protected override string BuildRepr() => $"[[{String.Join(",", Label)}]]{Inner}";

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
