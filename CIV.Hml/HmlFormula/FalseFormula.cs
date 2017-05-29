using CIV.Interfaces;


namespace CIV.Hml
{
    public class FalseFormula : IHmlFormula
    {
        public bool Check(IProcess process) => false;
    }
}
