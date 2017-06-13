using System;
using System.Collections.Generic;
using CIV.Common;

namespace CIV.Hml
{
    class NegFormula : HmlFormula
    {
        public HmlFormula Inner { get; set; }
        public override bool Check(IProcess process) => !(Inner.Check(process));

        public override IEnumerable<HmlFormula> GetSubformulae()
        {
            yield return Inner;
            foreach (var sub in Inner.GetSubformulae())
            {
                yield return sub;
            }
        }

        protected override string BuildRepr() => $"not {Inner}";
    }
}
