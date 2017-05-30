using System.Collections.Generic;
using CIV.Interfaces;

namespace CIV.Ccs
{
    class ChoiceProcess : CcsProcess
    {
        public CcsProcess Inner1 { get; set; }
        public CcsProcess Inner2 { get; set; }

        public override IEnumerable<Transition> Transitions()
        {
            foreach (var t in Inner1.Transitions())
                yield return t;
            foreach (var t in Inner2.Transitions())
                yield return t;
        }

        public override bool Equals(CcsProcess other)
        {
            var otherChoice = other as ChoiceProcess;
            return otherChoice != null &&
                (Inner1.Equals(otherChoice.Inner1) &&
                 Inner2.Equals(otherChoice.Inner2))
                ||
                (Inner1.Equals(otherChoice.Inner2) &&
                 Inner2.Equals(otherChoice.Inner1));
        }
        public override int GetHashCode() => Inner1.GetHashCode() * Inner2.GetHashCode();
    }
}