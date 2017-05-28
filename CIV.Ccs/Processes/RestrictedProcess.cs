﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace CIV.Ccs
{
    public class RestrictedProcess : IProcess
    {
        public IProcess Inner { get; set; }
        public ISet<String> Restrictions { get; set; }

        public IEnumerable<Transition> Transitions()
        {
            return (from t in Inner.Transitions()
                    where
                    t.Label == Const.tau ||
                    !(Restrictions.Contains(t.Label) ||
                      Restrictions.Contains(t.Label.Coaction()))
                    select new Transition
                    {
                        Label = t.Label,
                        Process = new RestrictedProcess
                        {
                            Inner = t.Process,
                            Restrictions = Restrictions
                        }
                    });
        }
    }
}