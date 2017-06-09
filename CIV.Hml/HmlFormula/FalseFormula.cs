using System;
using CIV.Common;

namespace CIV.Hml
{
    class FalseFormula : HmlFormula
    {
        public override bool Check(IProcess process) => false;

        protected override string BuildRepr()
        {
            return "ff";
        }
    }
}
