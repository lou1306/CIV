using System;
namespace CIV.Hml
{
    class HmlListener : HmlParserBaseListener
    {
        public HmlFormula RootFormula { get; private set; }

        readonly HmlFactory factory = new HmlFactory();

        public override void EnterBaseHml(HmlParser.BaseHmlContext context)
        {
            RootFormula = factory.Create(context.hml());
        }
    }
}
