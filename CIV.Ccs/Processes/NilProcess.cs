using System.Collections.Generic;
using System.Linq;

namespace CIV.Ccs
{
    class NilProcess : IProcess
    {
        public IEnumerable<Transition> Transitions()
        {
            return Enumerable.Empty<Transition>();
        }
    }
}
