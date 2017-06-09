using CIV.Common;

namespace CIV.Hml
{
    class TrueFormula : HmlFormula
    {
        protected override string BuildRepr() => "tt";

        public override bool Check(IProcess process) => true;
    }
}
