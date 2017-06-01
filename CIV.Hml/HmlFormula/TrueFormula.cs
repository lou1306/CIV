using CIV.Interfaces;

namespace CIV.Hml
{
    class TrueFormula : IHmlFormula
    {
        public bool Check(IProcess process) => true;
    }
}
