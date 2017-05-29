using System.Collections.Generic;
using static CIV.Ccs.CcsParser;
using CIV.Interfaces;

namespace CIV.Ccs
{
    /// <summary>
    /// Proxy class that delays the creation of a new Process instance until it
    /// is needed.
    /// </summary>
    class ProcessProxy : Proxy<CcsProcess, ProcessContext>, IHasWeakTransitions
    {
        public ProcessProxy(IFactory<CcsProcess, ProcessContext> factory, ProcessContext context) : base(factory, context)
        {
        }

        IEnumerable<Transition> IProcess.Transitions() => Real.Transitions();

        IEnumerable<Transition> IHasWeakTransitions.WeakTransitions()
        {
            return Real.WeakTransitions();
        }
    }
}
