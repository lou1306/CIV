using System;
using CIV.Processes;

namespace CIV.HmlCheck
{
    public class NegFormula : IHmlFormula
    {
        public IHmlFormula Inner { get; set; }
        public bool Check(IProcess process) => !(Inner.Check(process));
    }
}
