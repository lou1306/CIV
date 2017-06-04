using System;
using System.Collections.Generic;
using System.Linq;
using CIV.Interfaces;

namespace CIV.Ccs
{
    /// <summary>
    /// Nil process. Implemented as a singleton class.
    /// </summary>
    class NilProcess : CcsProcess
    {
        public static NilProcess Instance { get; } = new NilProcess();

        NilProcess(){}

		protected override IEnumerable<Transition> EnumerateTransitions()
		{
            return Enumerable.Empty<Transition>();
        }
        public override bool Equals(CcsProcess other) => other is NilProcess;

        protected override string BuildRepr() => Const.nil;

	}
}
