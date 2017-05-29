using System;
using System.Collections.Generic;
using CIV.Interfaces;

namespace CIV.Ccs
{
    public class PrefixProcess : CcsProcess
    {
        public String Label { get; set; }
        public IProcess Inner { get; set; }

        public override IEnumerable<Transition> Transitions()
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
