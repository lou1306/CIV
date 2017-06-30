using System;
using System.Collections.Generic;
using CIV.Common;

namespace CIV.Hml
{
    class WeakBoxFormula : BoxFormula
    {
		protected override string BuildRepr()
		{
			if (!(Label is TopSet<string>))
            {
                return $"[[{String.Join(",", Label)}]]{Inner}";
            }
			return $"[[-]]{Inner}";
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
