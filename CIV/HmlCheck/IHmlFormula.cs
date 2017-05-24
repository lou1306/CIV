using System;
using CIV.Processes;

namespace CIV.HmlCheck
{
    public interface IHmlFormula
    {
        bool Check(IProcess process);
    }
}
