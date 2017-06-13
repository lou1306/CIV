using System.Linq;
using System.Collections.Generic;
using CIV.Common;

namespace CIV.Hml
{
    class TrueFormula : HmlFormula
    {
        protected override string BuildRepr() => "tt";

        public override bool Check(IProcess process) => true;
		
        public override IEnumerable<HmlFormula> GetSubformulae()
		{
			return Enumerable.Empty<HmlFormula>();
		}
    }
}
