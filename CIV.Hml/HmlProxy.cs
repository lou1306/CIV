using System;
using CIV.Interfaces;
using static CIV.Hml.HmlParser;
namespace CIV.Hml
{
    class HmlProxy : Proxy<IHmlFormula, HmlContext>, IHmlFormula
    {
        public HmlProxy(IFactory<IHmlFormula, HmlContext> factory, HmlContext context) : base(factory, context)
        {
        }

        public bool Check(IProcess process)
        {
            return Real.Check(process);
        }
    }
}
