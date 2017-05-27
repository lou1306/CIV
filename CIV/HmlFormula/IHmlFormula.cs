using System;
using CIV.Processes;

namespace CIV.HmlFormula
{
    public interface IHmlFormula
    {
        bool Check(IProcess process);
    }
}
