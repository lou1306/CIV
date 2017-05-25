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
                Inner = new Mock<IProcess>() as IProcess
            };
            Assert.Equal(1, process.Transitions().Count());
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
