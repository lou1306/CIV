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
    }
}
