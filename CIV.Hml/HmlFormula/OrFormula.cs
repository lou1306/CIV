using CIV.Common;

namespace CIV.Hml
{
    class OrFormula : HmlFormula
    {
		public HmlFormula Inner1 { get; set; }
		public HmlFormula Inner2 { get; set; }

        protected override string BuildRepr() => $"({Inner1} or {Inner2})";

        public override bool Check(IProcess process)
        {
			return Inner1.Check(process) || Inner2.Check(process);        
        }
    }
}
