using System;
using System.Collections.Generic;
using System.Linq;
using CIV.Interfaces;

namespace CIV.Ccs
{
    public static class Extensions
    {
        /// <summary>
        /// Coaction of the specified action.
        /// </summary>
        /// <returns>The coaction: 'action if action does not start
        /// with ' and vice versa</returns>
        /// <param name="action">A CCS action</param>
        public static String Coaction(this string action)
        {
            if (action == Const.tau) return action;
            return action.IsOutput() ? action.Substring(1) : $"'{action}";
        }

        /// <summary>
        /// Returns true if the argumnent is an "output action"
        /// (i.e. it is not tau and starts with ').
        /// </summary>
        /// <returns><c>true</c>, if action is output, <c>false</c> otherwise.</returns>
        /// <param name="action">An action.</param>
        public static bool IsOutput(this String action)
        {
            return action != Const.tau && action[0] == '\'';
        }
    
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
                where t.Label != Const.tau
                select t
            );
            foreach (var t in transitions.Where(x => x.Label == Const.tau))
            {
                result.Concat(t.Process.WeakTransitions());
            }
            return result;
        }
    }
}
