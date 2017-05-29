using System.Collections.Generic;
using System.Linq;
using CIV.Ccs;
using CIV.Interfaces;

namespace CIV.Hml
{
    public class DiamondFormula : HmlLabelFormula
    {
        protected override bool CheckStrategy(IEnumerable<IProcess> processes)
            => processes.Any(Inner.Check);
    }
}
