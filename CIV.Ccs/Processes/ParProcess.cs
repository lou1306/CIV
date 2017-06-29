using System;
using System.Linq;
using System.Collections.Generic;
using CIV.Common;

namespace CIV.Ccs
{
    class ParProcess : CcsProcess
    {
        public CcsProcess Inner1 { get; set; }
        public CcsProcess Inner2 { get; set; }

        protected override IEnumerable<Transition> EnumerateTransitions()
        {
            var transitions1 = Inner1.GetTransitions();
            var transitions2 = Inner2.GetTransitions();
            var result = (from t in transitions1
                          select new Transition
                          {
                              Label = t.Label,
                              Process = new ParProcess
                              {
                                  Inner1 = (CcsProcess)t.Process,
                                  Inner2 = Inner2
                              }
                          });
            var result2 = (from t in transitions2
                           select new Transition
                           {
                               Label = t.Label,
                               Process = new ParProcess
                               {
                                   Inner1 = Inner1,
                                   Inner2 = (CcsProcess)t.Process
                               }
                           });
            var result3 = (
                from t1 in transitions1
                where t1.Label != Const.tau
                join t2 in transitions2 on t1.Label equals t2.Label.Coaction()
                select new Transition
                {
                    Label = Const.tau,
                    Process = new ParProcess
                    {
                        Inner1 = (CcsProcess)t1.Process,
                        Inner2 = (CcsProcess)t2.Process
                    }
                }
                );
            return result.Concat(result2).Concat(result3);
        }

        protected override string BuildRepr()
        {
            var list = new List<String> { Inner1.ToString(), Inner2.ToString() };
            list.Sort();
            return String.Join(Const.par, list);
        }
    }
}

