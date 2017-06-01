using CIV.Ccs;
using CIV.Interfaces;

namespace CIV.Hml
{
    class AndFormula : IHmlFormula
    {
		public IHmlFormula Inner1 { get; set; }
		public IHmlFormula Inner2 { get; set; }

        public bool Check(IProcess process)
        {
            return Inner1.Check(process) && Inner2.Check(process);
        }
    }
}
