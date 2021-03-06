﻿﻿using System;
using System.Collections.Generic;
using CIV.Common;
using System.Linq;


namespace CIV.Ccs
{
    public abstract class CcsProcess : IHasWeakTransitions, IEquatable<CcsProcess>
    {

        public string Pid { get; set; }

        public virtual bool Equals(CcsProcess other)
        {
            return this == other || ToString() == other.ToString();
        }

        protected string _repr;

        protected abstract string BuildRepr();
        //protected static TransitionComparer comparer = new TransitionComparer();

        /// <summary>
        /// Gets the transitions.
        /// </summary>
        /// <remarks>
        /// This is virtual only to allow mocking in the CIV.Test project.
        /// We use Memoize() to store the elements as they are generated,
        /// so to limit the number of instantiated Transitions.
        /// </remarks>
        /// <returns>The transitions.</returns>
        public virtual IEnumerable<Transition> GetTransitions()
        {
            return EnumerateTransitions().Memoize();
        }

        protected abstract IEnumerable<Transition> EnumerateTransitions();

        public override string ToString() => Pid ?? _repr ?? (_repr = BuildRepr());

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
            var transitions = GetTransitions().Distinct();
            var queue = new Queue<Transition>(transitions);
            var visited = new HashSet<CcsProcess> { this };

            while (queue.Count > 0)
            {
                var t = queue.Dequeue();
                yield return t;
				CcsProcess nextProcess = (CcsProcess)t.Process;
				if (t.Label == Const.tau && ! visited.Contains(nextProcess))
				{
					visited.Add(nextProcess);
					nextProcess
					 .GetTransitions()
					 .ForEach(queue.Enqueue);
				}
			}
        }

        public bool Equals(IProcess other)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Transition> TauTransitions => GetTransitions()
            .Where(x => x.Label == Const.tau)
            .Distinct();
    }
}
