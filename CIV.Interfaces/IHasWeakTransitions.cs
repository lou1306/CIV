using System.Collections.Generic;
namespace CIV.Interfaces
{
    public interface IHasWeakTransitions
    {
		IEnumerable<Transition> WeakTransitions();
    }
}
