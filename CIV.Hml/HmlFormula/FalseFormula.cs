using System;
using System.Linq;
using System.Collections.Generic;
using CIV.Common;

namespace CIV.Hml
{
    class FalseFormula : HmlFormula
    {
        public override bool Check(IProcess process) => false;

        public override IEnumerable<HmlFormula> GetSubformulae()
        {
            return Enumerable.Empty<HmlFormula>();
        }

        protected override string BuildRepr()
        {
            return "ff";
        }
    }
}
