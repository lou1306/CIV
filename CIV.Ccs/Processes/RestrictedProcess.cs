using System;
using System.Linq;
using System.Collections.Generic;
using CIV.Interfaces;

namespace CIV.Ccs
{
    class RestrictedProcess : CcsProcess
    {
        public CcsProcess Inner { get; set; }
        public ISet<String> Restrictions { get; set; }

        public override bool Equals(CcsProcess other)
        {
            var otherRestricted = other as RestrictedProcess;
            return otherRestricted != null
                && Inner.Equals(otherRestricted.Inner)
                     && Restrictions.SetEquals(otherRestricted.Restrictions);
        }

        public override IEnumerable<Transition> Transitions()
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