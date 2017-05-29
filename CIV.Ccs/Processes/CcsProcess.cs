using System.Linq;
using System.Collections.Generic;
using CIV.Interfaces;
         
namespace CIV.Ccs
{
    public abstract class CcsProcess : IProcess, IHasWeakTransitions
    {
        public abstract IEnumerable<Transition> Transitions();

		public IEnumerable<Transition> WeakTransitions()
		{
			var transitions = Transitions();
			var result = (
				from t in transitions
				where t.Label != Const.tau
				select t
			);
			foreach (var t in transitions.Where(x => x.Label == Const.tau))
			{
				result.Concat(t.Process.WeakTransitions());
			}
			return result;
		}

    }
}
