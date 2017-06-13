using Xunit;
using System;
using CIV.Ccs;

namespace CIV.Test
{
    public class RelabelingFunctionTest
    {
        [Theory]
		[InlineData("action", "relabeled")]
		[InlineData("action", "tau")]
        public void ShouldRelabelCoaction(string action, string relabeled)
        {
            var relabeling = new RelabelingFunction
            {
                {action, relabeled}
            };
            Assert.Equal(2, relabeling.Count);
            Assert.Equal(relabeled, relabeling[action]);
            Assert.Equal(relabeled.Coaction(), relabeling[action.Coaction()]);
        }

        [Fact]
        public void ShouldNotRelabelTau()
        {
            var relabeling = new RelabelingFunction();
            Assert.Throws<ArgumentException>(
                () =>
                {
                    relabeling.Add("tau", "relabeled");
                });
        }
    }
}
