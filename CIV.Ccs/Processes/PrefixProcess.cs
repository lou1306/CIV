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
		protected override string BuildRepr()
		{
            return $"{Label}{Const.prefix}{Inner}";
        }

    }
}
