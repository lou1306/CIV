using System;
using System.Linq;
using CIV.Ccs;
using Xunit;
using Antlr4.Runtime;
using CIV.Processes;

namespace CIV.Test
{
    public class ProcessesTest
    {
        [Fact]
        public void Transitions_EmptyForNil()
        {
            var nil = new NilProcess();
            Assert.Equal(nil.Transitions().Count(), 0);
        }

        [Fact]
        public void PrefixShouldHaveOneTransition()
        {
            var label = "tau";
            var nil = new NilProcess();
            var proc = new PrefixProcess
            {
                Label = label,
                Inner = nil
            };
            var transitions = proc.Transitions().ToList();
            Assert.Equal(proc.Transitions().Count(), 1);
        }

        /// <summary>
        /// Pars the should have par transitions.
        /// </summary>
        [Fact]
        public void ParShouldHaveParTransitions()
        {
            
        }


   //     CIV.Ccs.CcsParser SetupParser(String text)
   //     {
   //         var inputStream = new AntlrInputStream(text);
			//var lexer = new CcsLexer(inputStream);
			//var tokens = new CommonTokenStream(lexer);
			//return new CcsParser(tokens);
        //}

    }
}
