using System.Linq;
using System.Collections.Generic;
using CIV.Common;

namespace CIV.Hml
{
    class NegFormula : HmlFormula
    {
        public HmlFormula Inner { get; set; }
        public override bool Check(IProcess process) => !(Inner.Check(process));

        public override IEnumerable<IProcess> O(IEnumerable<IProcess> current, IEnumerable<IProcess> all)
        {
            return all.Except(Inner.O(current, all));
        }

        protected override string BuildRepr() => $"not {Inner}";
    }
}
