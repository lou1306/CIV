using System;
using System.Collections.Generic;
using System.Linq;
using CIV.Ccs;

namespace CIV.HmlFormula
{
    public class BoxFormula : HmlLabelFormula
    {
		protected override bool CheckStrategy(IEnumerable<IProcess> processes)
			=> processes.All(Inner.Check);

		protected override IEnumerable<Transition> GetTransitions(IProcess process)
			=> process.Transitions();
    }
}
