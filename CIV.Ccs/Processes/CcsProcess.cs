using System;
using System.Collections.Generic;
using CIV.Interfaces;
using System.Linq;

namespace CIV.Ccs
{
    public abstract class CcsProcess : IHasWeakTransitions, IEquatable<CcsProcess>
    {
        public abstract bool Equals(CcsProcess other);

        public abstract IEnumerable<Transition> GetTransitions();

        public abstract override string ToString();

        public override int GetHashCode() => ToString().GetHashCode();

        /// <summary>
        /// Gets the weak transitions from this process.
        /// </summary>
        /// <remarks>
        /// The enumerator is "guarded": it does not visit any process
        /// more than once.
        /// Rationale: all actions that could have been reached have already
        /// been "yielded" during the first visit to that process.
        /// </remarks>
        /// <returns>The weak transitions.</returns>
        public IEnumerable<Transition> GetWeakTransitions()
        {
            var comparer = new TransitionComparer();
            var transitions = GetTransitions().Distinct(comparer);
            var queue = new Queue<Transition>(transitions);

            var visited = new HashSet<string> { ToString() };

            while (queue.Count > 0)
            {
                var t = queue.Dequeue();
                var processRepr = t.Process.ToString();
                if (!visited.Contains(processRepr))
                {
                    visited.Add(processRepr);
                    if (t.Label != Const.tau)
                    {
                        yield return t;
                    }
                    else
                    {
                        foreach (var t1 in t.Process.GetTransitions().Distinct(comparer))
                        {
                            queue.Enqueue(t1);
                        }
                    }
                }
            }
        }
    }
}
