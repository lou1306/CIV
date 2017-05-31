using System;
using System.Collections.Generic;
using CIV.Interfaces;
using static CIV.Hml.HmlParser;

namespace CIV.Hml
{
    public class HmlFactory : IFactory<IHmlFormula, HmlContext>
    {
        public IHmlFormula Create(HmlContext context)
        {
            switch (context)
            {
                case TrueContext c: return new TrueFormula();
                case FalseContext c: return new FalseFormula();
                case ConjContext c:
                    return new AndFormula
                    {
                        Inner1 = new HmlProxy(this, c.hml(0)),
                        Inner2 = new HmlProxy(this, c.hml(1))
                    };
                case DisjContext c:
                    return new OrFormula
					{
						Inner1 = new HmlProxy(this, c.hml(0)),
						Inner2 = new HmlProxy(this, c.hml(1))
					};
                case NegatedContext c:
                    return new NegFormula
                    {
                        Inner = new HmlProxy(this, c.hml())
                    };
                case ParenthContext c:
                    return Create(c.hml());
                case DiamondContext c:
                    Console.WriteLine(String.Join(",",CreateLabels(c.labelList())));
                    return new DiamondFormula
                    {
                        Label = CreateLabels(c.labelList()),
                        Inner = new HmlProxy(this, c.hml())
                    };
				case WeakDiamondContext c:
					return new WeakDiamondFormula
					{
						Label = CreateLabels(c.labelList()),
						Inner = new HmlProxy(this, c.hml())
					};
				case WeakBoxContext c:
					return new WeakBoxFormula
					{
						Label = CreateLabels(c.labelList()),
						Inner = new HmlProxy(this, c.hml())
					};
                default:
                    throw new NotSupportedException();
            }
        }

        ISet<string> CreateLabels(LabelListContext context)
        {
            var result = new HashSet<string> { context.label().GetText() };
            if (context.labelList() != null)
            {
                result.UnionWith(CreateLabels(context.labelList()));
            }
            return result;
        }

    }
}
