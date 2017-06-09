using System;
using System.Collections.Generic;
using CIV.Common;

namespace CIV.Ccs
{
    public class PidProcess : CcsProcess
    {
        public CcsProcess Inner { get; set; }
        public string Pid { get; set; }

        public override bool Equals(CcsProcess other)
        {
            var otherPid = other as PidProcess;
            if (otherPid != null)
            {
                return Pid == otherPid.Pid;
            }
            return Inner.Equals(other);
        }

		protected override IEnumerable<Transition> EnumerateTransitions()
        => Inner.GetTransitions();

        protected override string BuildRepr() => Pid;
	}
}
