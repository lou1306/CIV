using System;
using CIV.Processes;

namespace CIV.HmlCheck
{
    public class TrueFormula : IHmlFormula
    {
        public bool Check(IProcess process) => true;
    }
}
