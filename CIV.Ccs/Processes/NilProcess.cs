using System;
using System.Collections.Generic;
using System.Linq;

namespace CIV.Ccs
{
    public class NilProcess : IProcess
    {
        public IEnumerable<Transition> Transitions()
        {
            return Enumerable.Empty<Transition>();
        }
    }
}
