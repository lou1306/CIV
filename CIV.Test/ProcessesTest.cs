using System;
using System.Linq;
using CIV.Ccs;
using Xunit;
using Antlr4.Runtime;
using CIV.Processes;
using Moq;
using System.Collections.Generic;

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

        [Fact]
        public void ParProcessFollowsSemantics()
        {
            var transitions = SetupParProcess().Transitions();
			
		}

        /// <summary>
        /// Setup a ParProcess where inner processes can evolve together.
        /// </summary>
        /// <returns>The par process.</returns>
        ParProcess SetupParProcess(String action = "action")
        {
            var p1 = Mock.Of<IProcess>(
                p => p.Transitions() == new List<Transition> { SetupTransition(action) }
            );

			var p2 = Mock.Of<IProcess>(
                p => p.Transitions() == new List<Transition> { SetupTransition(action.Coaction()) }
			);

            return new ParProcess
            {
                Inner1 = p1,
                Inner2 = p2
            };
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
