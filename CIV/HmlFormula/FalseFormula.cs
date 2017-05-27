using System;
using CIV.Processes;

namespace CIV.HmlFormula
{
    public class FalseFormula : IHmlFormula
    {
        public bool Check(IProcess process) => false;
    }
}
