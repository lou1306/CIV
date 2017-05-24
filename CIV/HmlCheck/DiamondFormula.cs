using System;
using System.Linq;
using CIV.Processes;

namespace CIV.HmlCheck
{
    public class DiamondFormula : IHmlFormula
    {
        public String Label { get; set; }
		public IHmlFormula Inner { get; set; }

		public bool Check(IProcess process)
        {
            var processes = (from t in process.Transitions()
                             where t.Label == Label
                             select t.Process);
            return processes.Any(
                p => Inner.Check(p)
            );
        }
    }
}
