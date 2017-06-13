using CIV.Common;
using System.Collections.Generic;

namespace CIV.Hml
{
    public abstract class HmlFormula : IProperty
    {
        string _repr;

        protected abstract string BuildRepr();

        public override string ToString() => _repr ?? (_repr = BuildRepr());

        public abstract bool Check(IProcess process);

        public abstract IEnumerable<HmlFormula> GetSubformulae();
    }
}
