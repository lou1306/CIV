using System;
using System.Collections.Generic;

namespace CIV.Common
{
    public interface IProcess : IEquatable<IProcess>
    {
        IEnumerable<Transition> GetTransitions();

        string ToString();
    }
}
