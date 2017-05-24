using System;
using System.Linq;
using System.Collections.Generic;

namespace CIV.Processes
{
    public class RenamedProcess : IProcess
    {
        public IProcess Inner { get; set; }
        public IDictionary<String, String> Renamings { get; set; }

        public IEnumerable<Transition> Transitions()
        {
            var transitions = Inner.Transitions();
            return (from t in transitions
                    select RenamedTransition(t));
        }

        Transition RenamedTransition(Transition t)
        {
            var label = Renamings.ContainsKey(t.Label)
                                 ? Renamings[t.Label]
                                 : t.Label;
            return new Transition
            {
                Label = label,
                Process = new RenamedProcess
                {
                    Inner = t.Process,
                    Renamings = Renamings
                }
            };
        }
    }
}