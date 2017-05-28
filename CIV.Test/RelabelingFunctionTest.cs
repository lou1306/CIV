using Xunit;
using CIV.Ccs;

namespace CIV.Test
{
    public class RelabelingFunctionTest
    {
        [Fact]
        public void ShouldAddInputAction()
        {
            var relabeling = new RelabelingFunction
            {
                {"action", "relabeled"}
            };
            Assert.Equal(1, relabeling.Count);
            Assert.Equal("relabeled", relabeling["action"]);
        }
    }
}
