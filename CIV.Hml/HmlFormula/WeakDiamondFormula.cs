using System.Collections.Generic;
using System.Linq;
using CIV.Interfaces;
using CIV.Ccs;
using System;

namespace CIV.Hml
{
    public class WeakDiamondFormula : HmlLabelFormula
    {
        protected override bool CheckStrategy(IEnumerable<IProcess> processes)
        => processes.Any(Inner.Check);

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
