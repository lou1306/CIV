using System;
using System.Collections.Generic;
using System.Linq;

namespace CIV.Processes
{
    public class NilProcess : IProcess
    {
        public IEnumerable<Transition> Transitions()
        {
            return Enumerable.Empty<Transition>();
        }
    }
}
