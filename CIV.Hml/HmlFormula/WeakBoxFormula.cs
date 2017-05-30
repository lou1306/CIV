using System;
using System.Collections.Generic;
using System.Linq;
using CIV.Ccs;
using CIV.Interfaces;

namespace CIV.Hml
{
    public class WeakBoxFormula : HmlLabelFormula
    {

        protected override bool CheckStrategy(IEnumerable<IProcess> processes)
            => processes.All(Inner.Check);

        protected override IEnumerable<Transition> TransitionStrategy(IProcess process)
        {
            if (process is IHasWeakTransitions)
            {
                return ((IHasWeakTransitions)process).WeakTransitions();
            }
            throw new ArgumentException();
        }
    }
}
