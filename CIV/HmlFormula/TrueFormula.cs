using CIV.Ccs;

namespace CIV.HmlFormula
{
    public class TrueFormula : IHmlFormula
    {
        public bool Check(IProcess process) => true;
    }
}
