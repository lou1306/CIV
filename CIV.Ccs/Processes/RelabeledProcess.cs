using System.Linq;
using System.Collections.Generic;
using CIV.Interfaces;

namespace CIV.Ccs
{
    class RelabeledProcess : CcsProcess
    {
        public IProcess Inner { get; set; }
        public RelabelingFunction Relabeling { get; set; }

        public override IEnumerable<Transition> Transitions()
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