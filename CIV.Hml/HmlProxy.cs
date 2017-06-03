using System;
using CIV.Interfaces;
using static CIV.Hml.HmlParser;
namespace CIV.Hml
{
    class HmlProxy : HmlFormula
    {
        Proxy<HmlFormula, HmlContext> _proxy;

        public HmlProxy(IFactory<HmlFormula, HmlContext> factory, HmlContext context)
        {
            _proxy = new Proxy<HmlFormula, HmlContext>(factory, context);
        }

        public override bool Check(IProcess process)
        {
			return _proxy.Real.Check(process);
		}

        protected override string BuildRepr()
        {
            return _proxy.Real.ToString();
        }
    }
}
