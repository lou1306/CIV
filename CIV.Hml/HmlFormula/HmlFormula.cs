using CIV.Common;
using System.Collections.Generic;

namespace CIV.Hml
{
    public abstract class HmlFormula
    {
        string _repr;

        protected abstract string BuildRepr();

        public override string ToString() => _repr ?? (_repr = BuildRepr());

        public abstract bool Check(IProcess process);

        //public abstract IEnumerable<HmlFormula> GetSubformulae();

        public abstract IEnumerable<IProcess> O(IEnumerable<IProcess> current, IEnumerable<IProcess> all);
    }
}
