using System;
using System.Collections.Generic;

namespace CIV.Common
{
    public class Transition
    {
        public string Label { get; set; }
        public IProcess Process { get; set; }
    }

    public class TransitionComparer : IEqualityComparer<Transition>
    {
        public bool Equals(Transition x, Transition y)
        {
            var comp = new ProcessComparer();
            return x.Label == y.Label && comp.Equals(x.Process, y.Process);
        }

        public int GetHashCode(Transition obj)
        {
            return obj.Label.GetHashCode() * obj.Process.GetHashCode();
        }
    }
}
