using System;
using System.Linq;
using Xunit;
using CIV.Processes;
using Moq;
using System.Collections.Generic;
using CIV.Helpers;

namespace CIV.Test
{
    public class ProcessesTest
    {
        [Fact]
        public void NilProcessHasNoTransitions()
        {
            var nil = new NilProcess();
            Assert.Equal(0, nil.Transitions().Count());
        }

        [Fact]
        public void PrefixProcessHasOneTransition()
        {
            var process = new PrefixProcess
            {
                Label = "tau",
                Inner = Mock.Of<IProcess>()
            };
            Assert.Equal(1, process.Transitions().Count());
        }

        [Fact]
        public void ParProcessHasParTransitions()
        {
            var transitions = SetupParProcess().Transitions();
            foreach (var t in transitions)
            {
                Assert.Equal(typeof(ParProcess), t.Process.GetType());
            }
        }

        [Theory]
        [InlineData("action")]
        [InlineData("tau")]
        public void ParProcessFollowsSemantics(String action)
        {
            var transitions = SetupParProcess(action).Transitions().ToList();
            Assert.Equal(3, transitions.Count());
            switch (action)
            {
                case "tau":
                    Assert.Equal(3, transitions.Where(t => t.Label == action).Count());
                    break;
                default:
                    Assert.Equal(1, transitions.Where(t => t.Label == action).Count());
                    Assert.Equal(1, transitions.Where(t => t.Label == action.Coaction()).Count());
                    Assert.Equal(1, transitions.Where(t => t.Label == "tau").Count());
                    break;
            }
        }

        [Theory]
		[InlineData("action")]
		[InlineData("'action")]
        public void RestrictedProcessFollowsSemantics(String innerAction)
        {
            var restrictions = new RestrictionSet { innerAction };
            var process = new RestrictedProcess
            {
                Inner = Common.SetupMockProcess(innerAction),
                Restrictions = restrictions
            };
            var n = innerAction == "tau" ? 1 : 0;
            Assert.Equal(n, process.Transitions().Count());

			process = new RestrictedProcess
			{
                Inner = SetupParProcess(innerAction),
				Restrictions = restrictions
			};

			n = innerAction == "tau" ? 3 : 1;
			Assert.Equal(n, process.Transitions().Count());
		}

        [Fact]
        public void RestrictionSetRefusesTau()
        {
            Assert.Throws<ArgumentException>(
                () => new RestrictionSet { "tau" }
            );
        }


        /// <summary>
        /// Setup a ParProcess where inner processes can evolve together.
        /// </summary>
        /// <returns>The par process.</returns>
        ParProcess SetupParProcess(String action = "action")
        {
            return new ParProcess
            {
                Inner1 = Common.SetupMockProcess(action),
                Inner2 = Common.SetupMockProcess(action.Coaction())
            };
        }

    }
}
