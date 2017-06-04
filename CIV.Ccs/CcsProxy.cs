using System.Collections.Generic;
using static CIV.Ccs.CcsParser;
using CIV.Interfaces;
using System;

namespace CIV.Ccs
{
    /// <summary>
    /// Proxy class that delays the creation of a new Process instance until it
    /// is needed.
    /// </summary>
    class CcsProxy : CcsProcess
    {
        readonly Proxy<CcsProcess, ProcessContext> proxy;

        public CcsProxy(CcsFactory factory, ProcessContext context)
        {
            proxy = new Proxy<CcsProcess, ProcessContext>(factory, context);
        }

        public override bool Equals(CcsProcess other) => proxy.Real.Equals(other);

        public override int GetHashCode() => proxy.Real.GetHashCode();

		protected override IEnumerable<Transition> EnumerateTransitions()
        => proxy.Real.GetTransitions();

        protected override string BuildRepr() => proxy.Real.ToString();
    }
}
