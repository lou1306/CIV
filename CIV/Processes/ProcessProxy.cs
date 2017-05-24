using System;
using System.Collections.Generic;
using static CIV.Ccs.CcsParser;

namespace CIV.Processes
{
    public class ProcessProxy : IProcess
    {
        protected ProcessContext context;
        protected ProcessFactory factory;
        private IProcess _real;
        private IProcess RealProcess =>
            _real ?? (_real = factory.Create(context));
        public ProcessProxy(ProcessFactory factory, ProcessContext context)
        {
            this.factory = factory;
            this.context = context;
        }

        IEnumerable<Transition> IProcess.Transitions() =>
                                        RealProcess.Transitions();
    }
}
