using System;
using System.Collections.Generic;
namespace CIV.Processes
{
    public interface IProcess
    {
        IEnumerable<Transition> Transitions();
    }
}
