using System;
using System.Collections.Generic;
using CIV.Common;

namespace CIV.Ccs
{
    class ChoiceProcess : CcsProcess
    {
        public CcsProcess Inner1 { get; set; }
        public CcsProcess Inner2 { get; set; }


        protected override IEnumerable<Transition> EnumerateTransitions()
        {
			foreach (var t in Inner1.GetTransitions())
				yield return t;
			foreach (var t in Inner2.GetTransitions())
				yield return t;
        }

        protected override string BuildRepr()
        {
            var list = new List<String> { Inner1.ToString(), Inner2.ToString() };
            list.Sort();
            return String.Join(Const.choice, list);
        }
    }
}