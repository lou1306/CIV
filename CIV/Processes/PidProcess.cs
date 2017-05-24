using System;
using System.Collections.Generic;
using CIV.Ccs;
using static CIV.Ccs.CcsParser;

namespace CIV.Processes
{
    public class PidProcess : ProcessProxy
    {
        public PidProcess(ProcessFactory factory, ProcessContext context) : base(factory, context)
        {
        }

        public String Pid { get; set; }
    }
}
