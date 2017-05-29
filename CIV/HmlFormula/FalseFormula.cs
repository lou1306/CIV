using CIV.Ccs;

namespace CIV.HmlFormula
{
    public class FalseFormula : IHmlFormula
    {
        public bool Check(IProcess process) => false;
    }
}
