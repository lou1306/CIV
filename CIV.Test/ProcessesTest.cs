using System;
using System.Linq;
using CIV.Ccs;
using Xunit;
using Antlr4.Runtime;
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
        public void RestrictedProcessFollowsSemantics(String innerAction)
        {
            var restrictions = new RestrictionSet { innerAction };
            var process = new RestrictedProcess
            {
                Inner = SetupMockProcess(innerAction),
                Restrictions = restrictions
            };
            var n = innerAction == "tau" ? 1 : 0;
            Assert.Equal(n, process.Transitions().Count());

			process = new RestrictedProcess
			{
                Inner = SetupParProcess(innerAction),
				Restrictions = restrictions
			};

			n = innerAction == "tau" ? 3 : 0;
			Assert.Equal(n, process.Transitions().Count());
		}


        [Theory]
        [InlineData("action", "renamed")]
        [InlineData("'action", "renamed")]
        [InlineData("action", "'renamed")]
        [InlineData("'action", "'renamed")]
        //[InlineData("action", "tau")]
        public void RenamedProcessFollowsSemantics(String action, String renamed)
        {
            var process = SetupRenamedProcess(action, renamed);
            var transitions = process.Transitions();

			Assert.Equal(0, transitions.Where(t => t.Label == action).Count());
            Assert.Equal(1, transitions.Where(t => t.Label == renamed).Count());
        }

        [Theory]
		[InlineData("action", "renamed")]
        public void RenamedProcessHasRenamedTransitions(String action, String renamed)
        {
            var process = SetupRenamedProcess(action, renamed);
			var transitions = process.Transitions();

            foreach (var t in transitions)
            {
                Assert.Equal(t.Process.GetType(), typeof(RenamedProcess));
            }
        }


        RenamedProcess SetupRenamedProcess(String action, String renamed)
        {
			return new RenamedProcess
			{
				Inner = SetupMockProcess(action),
				Renamings = new Dictionary<String, String> {
					{ action, renamed }
				}
			};
        }

        /// <summary>
        /// Setup a ParProcess where inner processes can evolve together.
        /// </summary>
        /// <returns>The par process.</returns>
        ParProcess SetupParProcess(String action = "action")
        {
            return new ParProcess
            {
                Inner1 = SetupMockProcess(action),
                Inner2 = SetupMockProcess(action.Coaction())
            };
        }

        /// <summary>
        /// Setup a mock process that can only do the given action. 
        /// </summary>
        /// <returns>The mock process.</returns>
        /// <param name="action">Action.</param>
        IProcess SetupMockProcess(String action = "action")
        {
            return Mock.Of<IProcess>(
                p => p.Transitions() == new List<Transition> { SetupTransition(action) }
            );
        }

        Transition SetupTransition(String label)
        {
            return new Transition
            {
                Label = label,
                Process = Mock.Of<IProcess>()
            };
        }

        /// <summary>
        /// Pars the should have par transitions.
        /// </summary>
        //[Fact]
        //public void Transitions_Par()
        //{

        //}

    }
}
