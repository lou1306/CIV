using System;
using System.Collections.Generic;
using CIV.Interfaces;

namespace CIV.Ccs
{
    public abstract class CcsProcess : IHasWeakTransitions, IEquatable<CcsProcess>
    {
        public abstract bool Equals(CcsProcess other);

        public abstract IEnumerable<Transition> Transitions();

        public IEnumerable<Transition> WeakTransitions()
        {
            // https://stackoverflow.com/questions/3969963/when-not-to-use-yield-return
            var stack = new Queue<Transition>(Transitions());
            while (stack.Count > 0)
            {
                var t = stack.Dequeue();
                yield return t;
                if (t.Label == Const.tau)
                {
                    foreach (var t1 in t.Process.Transitions())
                    {
                        stack.Enqueue(t1);
                        //System.Console.WriteLine("Queueing {0}", t1.Label);
                    }
                }
            }
        }
    }
}
