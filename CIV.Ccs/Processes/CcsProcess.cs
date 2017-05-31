using System;
using System.Collections.Generic;
using CIV.Interfaces;

namespace CIV.Ccs
{
    public abstract class CcsProcess : IHasWeakTransitions, IEquatable<CcsProcess>
    {
        public abstract bool Equals(CcsProcess other);

        public abstract IEnumerable<Transition> GetTransitions();

        public abstract override string ToString();

        public override int GetHashCode() => ToString().GetHashCode();

        /// <summary>
        /// Retuns an enumerator of weak transitions from this process. 
        /// </summary>
        /// <remarks>
        /// Notice that this enumerator could be infinite, so do not
        /// use it without some sort of stopping criterion.
        /// </remarks>
        /// <returns>An enumerator of weak transitions.</returns>
        public IEnumerable<Transition> GetWeakTransitions()
        {
            var queue = new Queue<Transition>(GetTransitions());
            while (queue.Count > 0)
            {
                var t = queue.Dequeue();
                yield return t;
                if (t.Label == Const.tau)
                {
                    foreach (var t1 in t.Process.GetTransitions())
                    {
                        queue.Enqueue(t1);
                    }
                }
            }
        }
    }
}
