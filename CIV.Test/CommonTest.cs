using Xunit;
using System.Collections.Generic;
using CIV.Common;

namespace CIV.Test
{
    public class CommonTest
    {
        [Fact]
        public void TopSetContainsEveryElement()
        {
            var top = new TopSet<string>();
            Assert.True(top.Contains("action"));

        }

        [Fact]
        public void TopSetContainsEverySet()
        {
			var top = new TopSet<string>();
			var s = new HashSet<string> { "action" };
			Assert.True(top.IsSupersetOf(s));
            Assert.True(top.Overlaps(s));
			Assert.True(top.IsProperSupersetOf(s));
			Assert.False(top.IsSubsetOf(s));
			Assert.False(top.IsProperSubsetOf(s));
        }

    }
}
