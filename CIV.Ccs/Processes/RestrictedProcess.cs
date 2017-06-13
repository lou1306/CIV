using System;
using System.Linq;
using System.Collections.Generic;
using CIV.Common;

namespace CIV.Ccs
{
    class RestrictedProcess : CcsProcess
    {
        public CcsProcess Inner { get; set; }
        public ISet<String> Restrictions { get; set; }

		protected override IEnumerable<Transition> EnumerateTransitions()
		{
            return (from t in Inner.GetTransitions()
                    where
                    t.Label == Const.tau ||
                    !(Restrictions.Contains(t.Label) ||
                      Restrictions.Contains(t.Label.Coaction()))
                    select new Transition
                    {
                        Label = t.Label,
                        Process = new RestrictedProcess
                        {
                            Inner = (CcsProcess) t.Process,
                            Restrictions = Restrictions
                        }
                    });
        }

		protected override string BuildRepr()
		{
            return String.Format(Const.restrictFormat, Inner, Restrictions);
		}
    }
}