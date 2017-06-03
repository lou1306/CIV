using Xunit;
using CIV.Hml;
using CIV.Ccs;
using CIV.Interfaces;
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
            var process = new PrefixProcess
            {
                Inner = NilProcess.Instance,
                Label = "action"
            };
            var formula = new BoxFormula
            {
                Inner = new FalseFormula(),
                Label = new HashSet<string>{ "anotherAction" }
            };
            Assert.True(formula.Check(process));
        }


	}
}
