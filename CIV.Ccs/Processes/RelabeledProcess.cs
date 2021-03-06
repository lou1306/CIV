﻿using System.Linq;
using System.Collections.Generic;
using CIV.Common;
using System;

namespace CIV.Ccs
{
    class RelabeledProcess : CcsProcess
    {
        public CcsProcess Inner { get; set; }
        public RelabelingFunction Relabeling { get; set; }

		protected override IEnumerable<Transition> EnumerateTransitions()
		{
            return (from t in Inner.GetTransitions()
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
                    Inner = (CcsProcess) t.Process,
                    Relabeling = Relabeling
                }
            };
        }

		protected override string BuildRepr()
		{
            return String.Format(Const.relabelFormat, Inner, Relabeling);
        }
    }
}