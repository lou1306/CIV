using System.Collections.Generic;
namespace CIV.Common
{
    public interface IHasWeakTransitions : IProcess
    {
		IEnumerable<Transition> GetWeakTransitions();
    }
}
