using System;
using System.Collections.Generic;
using CIV.Common;

namespace CIV.Hml
{
    class AndFormula : HmlFormula
    {
		public HmlFormula Inner1 { get; set; }
		public HmlFormula Inner2 { get; set; }

        public override bool Check(IProcess process)
        {
            return Inner1.Check(process) && Inner2.Check(process);
        }

        public override IEnumerable<HmlFormula> GetSubformulae()
        {
            yield return Inner1;
            yield return Inner2;
            foreach (var subformula in Inner1.GetSubformulae())
            {
                yield return subformula;
            }
			foreach (var subformula in Inner2.GetSubformulae())
			{
				yield return subformula;
			}
        }

        protected override string BuildRepr() => $"({Inner1} and {Inner2})";


    }
}
