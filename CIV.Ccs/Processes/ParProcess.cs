using System.Linq;
using System.Collections.Generic;
using CIV.Interfaces;

namespace CIV.Ccs
{
    class ParProcess : CcsProcess
    {
        public CcsProcess Inner1 { get; set; }
        public CcsProcess Inner2 { get; set; }

        public override IEnumerable<Transition> Transitions()
        {
            var transitions1 = Inner1.Transitions();
            var transitions2 = Inner2.Transitions();
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
            foreach (var t in result)
                yield return t;

            result = (from t in transitions2
                      select new Transition
                      {
                          Label = t.Label,
                          Process = new ParProcess
                          {
                              Inner1 = Inner1,
                              Inner2 = (CcsProcess)t.Process
                          }
                      });
            foreach (var t in result)
                yield return t;
            result = (from t1 in transitions1
                      join t2 in transitions2
                      on t1.Label equals t2.Label.Coaction()
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
            foreach (var t in result)
                yield return t;
        }
        public override bool Equals(CcsProcess other)
        {
            var otherPar = other as ParProcess;
            return otherPar != null &&
                (Inner1.Equals(otherPar.Inner1) &&
                 Inner2.Equals(otherPar.Inner2))
                ||
                (Inner1.Equals(otherPar.Inner2) &&
                 Inner2.Equals(otherPar.Inner1));
        }
        public override int GetHashCode() => Inner1.GetHashCode() * Inner2.GetHashCode();
    }
}

