using System;
using System.Collections.Generic;
using CIV.Common;
using System.Linq;

namespace CIV.Ccs
{
    class ChoiceProcess : CcsProcess
    {
        public CcsProcess Inner1 { get; set; }
        public CcsProcess Inner2 { get; set; }


        protected override IEnumerable<Transition> EnumerateTransitions()
        {
            return Inner1.GetTransitions().Concat(Inner2.GetTransitions());
        }

        protected override string BuildRepr()
        {
            var list = new List<String> { Inner1.ToString(), Inner2.ToString() };
            list.Sort();
            return String.Join(Const.choice, list);
        }
    }
}