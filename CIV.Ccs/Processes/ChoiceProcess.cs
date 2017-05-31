using System;
using System.Collections.Generic;
using CIV.Interfaces;

namespace CIV.Ccs
{
    class ChoiceProcess : CcsProcess
    {
        public CcsProcess Inner1 { get; set; }
        public CcsProcess Inner2 { get; set; }

        public override IEnumerable<Transition> GetTransitions()
        {
            foreach (var t in Inner1.GetTransitions())
                yield return t;
            foreach (var t in Inner2.GetTransitions())
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

        public override string ToString()
        {
            var list = new List<String> { Inner1.ToString(), Inner2.ToString() };
            list.Sort();
            return String.Join(Const.choice, list);
                               
        }
    }
}