using System;

namespace CIV.Ccs
{
    public class Transition
    {
        public String Label { get; set; }
        public IProcess Process { get; set; }
    }
}
