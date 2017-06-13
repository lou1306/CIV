using Xunit;
using CIV.Hml;
using CIV.Ccs;

namespace CIV.Test
{
    public class HmlLabelFormulaTest
    {
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
			Assert.True(formula.Check(process));

			formula = HmlFacade.ParseAll("<<a>><<c>>tt;");
			Assert.True(formula.Check(process));
		}

		[Fact]
		public void WeakBoxFormulaFollowsSemantics()
		{
			var processDict = CcsFacade.ParseAll("P = tau.a.((tau.b.0) + b.tau.0);");
			var process = processDict["P"];

			var formula = HmlFacade.ParseAll("[[a]]<<b>>tt;");
			Assert.IsType(typeof(WeakBoxFormula), formula);
			Assert.True(formula.Check(process));

			formula = HmlFacade.ParseAll("[[b]]ff;");
			Assert.True(formula.Check(process));
		}

		[Fact]
		public void WeakBoxAllFormulaFollowsSemantics()
		{
			var processDict = CcsFacade.ParseAll("P = tau.a.((tau.b.0) + b.tau.0);");
			var process = processDict["P"];

            var formula = HmlFacade.ParseAll("[[-]](<<a>>tt or <<b>>tt);");
            Assert.IsType(typeof(WeakBoxFormula), formula);
			Assert.True(formula.Check(process));
		}

		[Fact]
		public void WeakDiamondAllFormulaFollowsSemantics()
		{
			var processDict = CcsFacade.ParseAll("P = tau.a.((tau.b.0) + b.tau.0);");
			var process = processDict["P"];

			var formula = HmlFacade.ParseAll("<<->><<a>>tt;");
            Assert.IsType(typeof(WeakDiamondFormula), formula);
			Assert.True(formula.Check(process));
		}
    }
}
