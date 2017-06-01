using CIV.Interfaces;


namespace CIV.Hml
{
    class FalseFormula : IHmlFormula
    {
        public bool Check(IProcess process) => false;
    }
}
