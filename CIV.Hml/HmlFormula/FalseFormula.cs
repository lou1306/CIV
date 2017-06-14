using System;
using System.Linq;
using System.Collections.Generic;
using CIV.Common;

namespace CIV.Hml
{
    class FalseFormula : HmlFormula
    {
        public override bool Check(IProcess process) => false;

        public override IEnumerable<IProcess> O(IEnumerable<IProcess> current, IEnumerable<IProcess> all)
        {
            return Enumerable.Empty<IProcess>();
        }

        protected override string BuildRepr()
        {
            return "ff";
        }
    }
}
