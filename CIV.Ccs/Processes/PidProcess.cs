using System;
using System.Collections.Generic;
using CIV.Common;

namespace CIV.Ccs
{
    public class PidProcess : CcsProcess
    {
        public CcsProcess Inner { get; set; }
        public string Pid { get; set; }

		protected override IEnumerable<Transition> EnumerateTransitions()
        => Inner.GetTransitions();

        protected override string BuildRepr() => Pid;

        public override bool Equals(CcsProcess other)
        {
            if (this == other) return true;
            var otherPid = other as PidProcess;
            return (otherPid?.Inner.Equals(Inner)).GetValueOrDefault(Inner.Equals(other));
        }
	}
}
