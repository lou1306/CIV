using System;
using System.Linq;
using System.Collections.Generic;

namespace CIV.Ccs
{
	public class ChoiceProcess : IProcess
	{
		public IProcess Inner1 { get; set; }
		public IProcess Inner2 { get; set; }

        public IEnumerable<Transition> Transitions()
		{
            return Inner1.Transitions().Union(Inner2.Transitions());
		}
	}
}