using System;
using System.Collections.Generic;

namespace CIV.Ccs
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
