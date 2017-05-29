using System.Collections.Generic;
using static CIV.Ccs.CcsParser;
using CIV.Interfaces;

namespace CIV.Ccs
{
    /// <summary>
    /// Proxy class that delays the creation of a new Process instance until it
    /// is needed.
    /// </summary>
    class ProcessProxy : CcsProcess
    {
        protected ProcessContext context;
        protected ProcessFactory factory;
        IProcess _real;
        IProcess RealProcess => _real ?? (_real = factory.Create(context));
        public ProcessProxy(ProcessFactory factory, ProcessContext context)
        {
            this.factory = factory;
            this.context = context;
        }

        public override IEnumerable<Transition> Transitions() =>
                                        RealProcess.Transitions();
    }
}
