using CIV.Interfaces;

namespace CIV.Hml
{
    class NegFormula : IHmlFormula
    {
        public IHmlFormula Inner { get; set; }
        public bool Check(IProcess process) => !(Inner.Check(process));
    }
}
