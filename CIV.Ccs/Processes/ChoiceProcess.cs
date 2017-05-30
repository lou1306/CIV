using System.Collections.Generic;
using CIV.Interfaces;

namespace CIV.Ccs
{
    class ChoiceProcess : CcsProcess
    {
        public IProcess Inner1 { get; set; }
        public IProcess Inner2 { get; set; }

        public override IEnumerable<Transition> Transitions()
        {
            foreach (var t in Inner1.Transitions())
                yield return t;
            foreach (var t in Inner2.Transitions())
                yield return t;
        }

        public override bool Equals(CcsProcess other)
        {
            var choice = other as ChoiceProcess;
            return choice != null &&
                (Inner1.Equals(choice.Inner1) &&
                 Inner2.Equals(choice.Inner2))
                ||
                (Inner1.Equals(choice.Inner2) &&
                 Inner2.Equals(choice.Inner1));
        }
        public override int GetHashCode() => Inner1.GetHashCode() * Inner2.GetHashCode();
    }
}