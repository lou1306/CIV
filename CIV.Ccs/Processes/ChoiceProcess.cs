using System;
using System.Linq;
using System.Collections.Generic;
using CIV.Interfaces;

namespace CIV.Ccs
{
    public class ChoiceProcess : CcsProcess
	{
		public IProcess Inner1 { get; set; }
		public IProcess Inner2 { get; set; }

        public override IEnumerable<Transition> Transitions()
		{
            return Inner1.Transitions().Union(Inner2.Transitions());
		}
	}
}