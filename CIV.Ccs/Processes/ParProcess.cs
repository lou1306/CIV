using System.Linq;
using System.Collections.Generic;
using CIV.Interfaces;

namespace CIV.Ccs
{
    class ParProcess : CcsProcess
    {
        public IProcess Inner1 { get; set; }
        public IProcess Inner2 { get; set; }

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
                                  Inner1 = t.Process,
                                  Inner2 = Inner2
                              }
                          })
                .Union(from t in transitions2
                       select new Transition
                       {
                           Label = t.Label,
                           Process = new ParProcess
                           {
                               Inner1 = Inner1,
                               Inner2 = t.Process
                           }
                       })
                .Union(from t1 in transitions1
                       join t2 in transitions2
                       on t1.Label equals t2.Label.Coaction()
                       select new Transition
                       {
                           Label = Const.tau,
                           Process = new ParProcess
                           {
                               Inner1 = t1.Process,
                               Inner2 = t2.Process
                           }
                       }
                );
            return result;
        }
    }
}

