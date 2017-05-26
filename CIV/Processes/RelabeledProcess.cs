using System;
using System.Linq;
using System.Collections.Generic;
using CIV.Helpers;

namespace CIV.Processes
{
    public class RelabeledProcess : IProcess
    {
        public IProcess Inner { get; set; }
        public RelabelingFunction Relabeling { get; set; }

        public IEnumerable<Transition> Transitions()
        {
            var transitions = Inner.Transitions();
            return (from t in transitions
                    select RenamedTransition(t));
        }

        Transition RenamedTransition(Transition t)
        {
            var label = Relabeling.ContainsKey(t.Label)
                                 ? Relabeling[t.Label]
                                 : t.Label;
            return new Transition
            {
                Label = label,
                Process = new RelabeledProcess
                {
                    Inner = t.Process,
                    Relabeling = Relabeling
                }
            };
        }
    }
}