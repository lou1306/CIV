﻿using System;
using System.Collections.Generic;
using CIV.Common;
using static CIV.Hml.HmlParser;

namespace CIV.Hml
{
    class HmlFactory : IFactory<HmlFormula, HmlContext>
    {
        readonly FalseFormula _false = new FalseFormula();
        readonly TrueFormula _true = new TrueFormula();
        readonly ISet<string> _top = new TopSet<string>();

        public HmlFormula Create(HmlContext context)
        {
            switch (context)
            {
                case TrueContext c: return _true;
                case FalseContext c: return _false;
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
                    return new DiamondFormula
                    {
                        Label = CreateLabels(c.labelList()),
                        Inner = new HmlProxy(this, c.hml())
                    };
				case BoxContext c:
                    return new BoxFormula
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
                case BoxAllContext c:
                    return new BoxFormula
                    {
                        Label = _top,
                        Inner = new HmlProxy(this, c.hml())
                    };
                case DiamondAllContext c:
                    return new DiamondFormula
					{
						Label = _top,
						Inner = new HmlProxy(this, c.hml())
					};
                case WeakBoxAllContext c:
					return new WeakBoxFormula
					{
						Label = _top,
						Inner = new HmlProxy(this, c.hml())
					};
                case WeakDiamondAllContext c:
                    return new WeakDiamondFormula
					{
						Label = _top,
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
