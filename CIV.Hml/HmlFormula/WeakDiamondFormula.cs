using System.Collections.Generic;
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
            return $"<<->>{Inner}";
        }
		protected override IEnumerable<Transition> TransitionStrategy(IProcess process)
		{
			try
			{
				return ((IHasWeakTransitions)process).GetWeakTransitions();
			}
            catch(InvalidCastException)
            {
                throw new ArgumentException();
            }
		}
    }
}
