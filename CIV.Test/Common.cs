using System;
using System.Collections.Generic;
using CIV.Ccs;
using CIV.Common;
using Moq;

namespace CIV.Test
{
    public static class Common
    {
        /// <summary>
        /// Setup a mock process that can only do the given action. 
        /// </summary>
        /// <returns>The mock process.</returns>
        /// <param name="action">Action.</param>
        public static CcsProcess SetupMockProcess(String action = "action")
        {
            return Mock.Of<CcsProcess>(p => p.GetTransitions() == new List<Transition>
            {
                SetupTransition(action)
            }
            );
        }

		static Transition SetupTransition(String label)
		{
			return new Transition
			{
				Label = label,
				Process = Mock.Of<CcsProcess>()
			};
		}

	}
}
