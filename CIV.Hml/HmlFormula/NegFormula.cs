using CIV.Interfaces;

namespace CIV.Hml
{
    public class NegFormula : IHmlFormula
    {
        public IHmlFormula Inner { get; set; }
        public bool Check(IProcess process) => !(Inner.Check(process));
    }
}
