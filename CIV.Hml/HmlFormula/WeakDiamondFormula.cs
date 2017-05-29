using System.Collections.Generic;
using System.Linq;
using CIV.Interfaces;
using CIV.Ccs;

namespace CIV.Hml
{
    public class WeakDiamondFormula : HmlLabelFormula
    {
        protected override bool CheckStrategy(IEnumerable<IProcess> processes)
			=> processes.Any(Inner.Check);
    }
}
