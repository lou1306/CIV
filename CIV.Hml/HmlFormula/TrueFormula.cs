using CIV.Interfaces;

namespace CIV.Hml
{
    public class TrueFormula : IHmlFormula
    {
        public bool Check(IProcess process) => true;
    }
}
