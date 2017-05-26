using System;
using System.Collections.Generic;
using System.Linq;

namespace CIV.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// Coaction of the specified action.
        /// </summary>
        /// <returns>The coaction: 'action if action does not start
        /// with ' and vice versa</returns>
        /// <param name="action">A CCS action</param>
        public static String Coaction(this String action)
        {
            if (action == "tau") return "tau";
            return action.StartsWith("'", StringComparison.InvariantCultureIgnoreCase) ?
                       action.Substring(1) : String.Format("'{0}", action);
        }

        /// <summary>
        /// Returns true if the argumnent is an "output action"
        /// (i.e. it is not tau and starts with ').
        /// </summary>
        /// <returns><c>true</c>, if action is output, <c>false</c> otherwise.</returns>
        /// <param name="action">An action.</param>
        public static bool IsOutput(this String action)
        {
            return action != "tau" && action.StartsWith("'", StringComparison.InvariantCulture);
        }
    }
}

namespace CIV.Processes
{
    public static class Extensions
    {
        /// <summary>
        /// Returns weak transitions for the given process.
        /// Weak transitions:
        /// <list type="number">
        /// <item>Have a non-tau label;</item>
        /// <item>Can be reached directly or with any number of tau moves.</item>
        /// </list>
        /// </summary>
        /// <returns>The weak transitions.</returns>
        /// <param name="process">A process.</param>
        public static IEnumerable<Transition> WeakTransitions(this IProcess process)
        {
            var transitions = process.Transitions();
            var result = (
                from t in transitions
                where t.Label != "tau"
                select t
            );
            foreach (var t in transitions.Where(x => x.Label == "tau"))
            {
                result.Concat(t.Process.WeakTransitions());
            }
            return result;
        }
    }
}
