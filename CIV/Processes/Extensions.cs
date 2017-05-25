using System;
using System.Collections.Generic;
using System.Linq;
namespace CIV.Processes
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

        public static IEnumerable<Transition> WeakTransitions(this IProcess process)
        {
            var transitions = process.Transitions();
            var result = (
                from t in transitions
                where t.Label != "tau"
                select t
            );
            foreach(var t in transitions.Where(x => x.Label == "tau"))
            {
                result.Concat(t.Process.WeakTransitions());
            }
            return result;
        }

    }
}
