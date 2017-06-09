using System;
using CIV.Ccs;
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

        protected override string BuildRepr() => $"({Inner1} and {Inner2})";
    }
}
