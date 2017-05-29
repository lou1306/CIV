using System.Collections.Generic;
using System.Linq;
using CIV.Interfaces;

namespace CIV.Ccs
{
    class NilProcess : CcsProcess
    {
        public override IEnumerable<Transition> Transitions()
        {
            return Enumerable.Empty<Transition>();
        }
    }
}
