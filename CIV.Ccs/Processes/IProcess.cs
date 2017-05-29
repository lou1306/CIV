using System;
using System.Collections.Generic;
namespace CIV.Ccs
{
    public interface IProcess
    {
        IEnumerable<Transition> Transitions();
    }
}
