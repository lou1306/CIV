using System.Collections.Generic;

namespace CIV.Interfaces
{
    public interface IProcess
    {
        IEnumerable<Transition> Transitions();
    }
}
