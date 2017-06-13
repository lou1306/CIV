using Xunit;
using CIV.Hml;
using CIV.Ccs;
using CIV.Common;
using Moq;
using System.Collections.Generic;

namespace CIV.Test
{
    public class HmlTest
    {

        [Fact]
        public void TrueFormulaAlwaysHolds()
        {
            var formula = new TrueFormula();
            Assert.True(formula.Check(Mock.Of<IProcess>()));
        }

		[Fact]
        public void FalseFormulaNeverHolds()
		{
            var formula = new FalseFormula();
            Assert.False(formula.Check(Mock.Of<IProcess>()));
		}

        [Theory]
		[InlineData(true, true, true)]
		[InlineData(true, false, false)]
		[InlineData(false, true, false)]
		[InlineData(false, false, false)]
        public void AndFormulaFollowsSemantics(bool inner1, bool inner2, bool expected){
            var formula = new AndFormula
            {
                Inner1 = inner1 ? new TrueFormula() : new FalseFormula() as HmlFormula,
                Inner2 = inner2 ? new TrueFormula() : new FalseFormula() as HmlFormula
            };
            Assert.Equal(expected, formula.Check(Mock.Of<IProcess>()));
        }

		[Theory]
		[InlineData(true, true, true)]
		[InlineData(true, false, true)]
		[InlineData(false, true, true)]
		[InlineData(false, false, false)]
		public void OrFormulaFollowsSemantics(bool inner1, bool inner2, bool expected)
		{
            var formula = new OrFormula
			{
				Inner1 = inner1 ? new TrueFormula() : new FalseFormula() as HmlFormula,
				Inner2 = inner2 ? new TrueFormula() : new FalseFormula() as HmlFormula
			};
			Assert.Equal(expected, formula.Check(Mock.Of<IProcess>()));
		}

        [Theory]
		[InlineData(true, false)]
        [InlineData(false, true)]
		public void NegFormulaFollowsSemantics(bool inner, bool expected)
        {
            var formula = new NegFormula
            {
                Inner = inner ? new TrueFormula() : new FalseFormula() as HmlFormula
            };
            Assert.Equal(expected, formula.Check(Mock.Of<IProcess>()));
        }

        [Fact]
        public void BoxFormulaFollowsSemantics()
        {
            var processDict = CcsFacade.ParseAll("P = action.0;");
            var process = processDict["P"];

			var formula = HmlFacade.ParseAll("[action]tt;");
            Assert.IsType(typeof(BoxFormula), formula);
			Assert.True(formula.Check(process));

			formula = HmlFacade.ParseAll("[anotherAction]ff;");
            Assert.True(formula.Check(process));

            formula = HmlFacade.ParseAll("[action,anotherAction]ff;");
            Assert.False(formula.Check(process));
			
            formula = HmlFacade.ParseAll("[action,anotherAction]tt;");
            Assert.True(formula.Check(process));
		}

		[Fact]
		public void DiamondFormulaFollowsSemantics()
		{
			var processDict = CcsFacade.ParseAll("P = action.0;");
			var process = processDict["P"];

			var formula = HmlFacade.ParseAll("<action>tt;");
			Assert.IsType(typeof(DiamondFormula), formula);
			Assert.True(formula.Check(process));

			formula = HmlFacade.ParseAll("<anotherAction>ff;");
            Assert.False(formula.Check(process));

			formula = HmlFacade.ParseAll("<action,anotherAction>tt;");
			Assert.True(formula.Check(process));
		}

		[Fact]
		public void WeakDiamondFormulaFollowsSemantics()
		{
            var processDict = CcsFacade.ParseAll("P = tau.a.((tau.b.0) + c.0);");
			var process = processDict["P"];

			var formula = HmlFacade.ParseAll("<<a>><<b>>tt;");

            Assert.IsType(typeof(WeakDiamondFormula), formula);
            //Assert.True(formula.Check(process));

            //formula = HmlFacade.ParseAll("<<a>><<c>>tt;");
            //Assert.True(formula.Check(process));
		}

	}
}
