using System;
using CIV.Interfaces;

namespace CIV.Hml
{
    class NegFormula : HmlFormula
    {
        public HmlFormula Inner { get; set; }
        public override bool Check(IProcess process) => !(Inner.Check(process));

        protected override string BuildRepr() => $"not {Inner}";
    }
}
