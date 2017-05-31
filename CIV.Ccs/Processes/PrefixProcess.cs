using System;
using System.Collections.Generic;
using CIV.Interfaces;

namespace CIV.Ccs
{
    class PrefixProcess : CcsProcess
    {
        public String Label { get; set; }
        public CcsProcess Inner { get; set; }

        public override bool Equals(CcsProcess other)
        {
            var otherPrefix = other as PrefixProcess;
            return
                otherPrefix != null &&
                (Label == otherPrefix.Label) &&
                Inner.Equals(otherPrefix.Inner);
        }

        public override IEnumerable<Transition> GetTransitions()
        {
            return new List<Transition>{
                new Transition{
                    Label = Label,
                    Process = Inner
                }
            };
        }
        public override string ToString()
        {
            return String.Format("{0}{1}{2}", Label, Const.prefix, Inner);
        }

    }
}
