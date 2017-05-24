using System;
using CIV.Processes;

namespace CIV.HmlCheck
{
    public class FalseFormula : IHmlFormula
    {
        public bool Check(IProcess process) => false;
    }
}
