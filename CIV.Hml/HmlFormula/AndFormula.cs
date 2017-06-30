using System;
using System.Collections.Generic;
using CIV.Common;

namespace CIV.Hml
{
    class AndFormula : HmlFormula
    {
		public HmlFormula Inner1 { get; set; }
		public HmlFormula Inner2 { get; set; }

        public override bool Check(IProcess process)
        {
            return Inner1.Check(process) && Inner2.Check(process);
        }

        protected override string BuildRepr() => $"({Inner1} and {Inner2})";

        public override IEnumerable<IProcess> O(IEnumerable<IProcess> current, IEnumerable<IProcess> all)
		{
            var set1 = new HashSet<IProcess>(Inner1.O(current, all));
            set1.IntersectWith(Inner2.O(current, all));
            return set1;
		}
    }
}
