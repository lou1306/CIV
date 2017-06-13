using System.Collections.Generic;
using System.Linq;
using CIV.Common;
using System;

namespace CIV.Hml
{
    class WeakDiamondFormula : DiamondFormula
    {

		protected override string BuildRepr()
		{
			if (!(Label is TopSet<string>))
				return $"<<{String.Join(",", Label)}>>{Inner}";
			else
				return $"<<->>{Inner}";
		}
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
