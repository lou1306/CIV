using System;
using System.Linq;
using System.Collections.Generic;
using CIV.Ccs;


namespace CIV.HmlFormula
{
    public abstract class HmlLabelFormula : IHmlFormula
    {
		public ISet<String> Label { get; set; }
		public IHmlFormula Inner { get; set; }

		public bool Check(IProcess process)
        {
            var processes = (from t in GetTransitions(process)
                             where Label.Contains(t.Label)
							 select t.Process);
            return CheckStrategy(processes);
        }

        protected abstract IEnumerable<Transition> GetTransitions(IProcess process);

        protected abstract bool CheckStrategy(IEnumerable<IProcess> processes);
    }
}
