using System;
using CIV.Ccs;
using System.Collections.Generic;
using Antlr4.Runtime;

namespace CIV.Processes
{
    public class PrefixProcess : IProcess
    {
        public String Label { get; set; }
        public IProcess Inner { get; set; }

        public IEnumerable<Transition> Transitions()
        {
            return new List<Transition>{
                new Transition{
                    Label = Label,
                    Process = Inner
                }
            };
        }
    }
}
