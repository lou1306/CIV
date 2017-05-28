using System;
using static CIV.Ccs.CcsParser;

namespace CIV.Ccs
{
    class PidProcess : ProcessProxy
    {
        public PidProcess(ProcessFactory factory, ProcessContext context) : 
        base(factory, context)
        {
        }

        public String Pid { get; set; }
    }
}
