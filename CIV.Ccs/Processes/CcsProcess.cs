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

            IEnumerable<Transition> nextInQueue;

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
                        nextInQueue = GetRecursiveTauTransitions(visited)
                            .Select(x => new Transition
                            {
                                Label = t.Label,
                                Process = x.Process
                            });
                    }
                    else
                    {
                        nextInQueue = t.Process.GetTransitions().Distinct(comparer);
                    }
					foreach (var t1 in nextInQueue)
					{
						queue.Enqueue(t1);
					}
                }
            }
        }

		IEnumerable<Transition> TauTransitions => GetTransitions().Where(x => x.Label == Const.tau);

        IEnumerable<Transition> GetRecursiveTauTransitions(ISet<string> visited = null)
        {
            var queue = new Queue<Transition>(TauTransitions);
            visited = visited ?? new HashSet<string> { ToString() };
            while (queue.Count > 0)
            {
                var t = queue.Dequeue();
                var procRepr = t.Process.ToString();
                if (!visited.Contains(procRepr))
                {
                    visited.Add(procRepr);
                    yield return t;
                    foreach (var item in t.Process.GetTransitions().Where(x => x.Label == Const.tau))
                    {
                        queue.Enqueue(item);
                    }
                }
            }

        }
    }
}
