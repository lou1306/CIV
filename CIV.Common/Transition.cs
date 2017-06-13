using System;
using System.Collections.Generic;

namespace CIV.Common
{
    public class Transition : IEquatable<Transition>
    {
        public string Label { get; set; }
        public IProcess Process { get; set; }

        public bool Equals(Transition other)
        {
            return Label == other.Label && Process.Equals(other.Process);
        }
    }
}
