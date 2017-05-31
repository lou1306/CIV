using System.Collections.Generic;
namespace CIV.Interfaces
{
    public interface IHasWeakTransitions : IProcess
    {
		IEnumerable<Transition> GetWeakTransitions();
    }
}
