using System;
using Antlr4.Runtime;
using CIV.Ccs;

namespace CIV.Processes
{
    public class Transition
    {
        public String Label { get; set; }
        public IProcess Process { get; set; }
    }
}
