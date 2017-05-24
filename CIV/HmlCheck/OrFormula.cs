using System;
using CIV.Processes;

namespace CIV.HmlCheck
{
    public class OrFormula : IHmlFormula
    {
		public IHmlFormula Inner1 { get; set; }
		public IHmlFormula Inner2 { get; set; }
        public bool Check(IProcess process) => 
            Inner1.Check(process) | Inner2.Check(process);
    }
}
