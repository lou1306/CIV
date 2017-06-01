﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CIV.Interfaces;

[assembly: InternalsVisibleTo("CIV.Test")]
namespace CIV.Ccs
{
    public static class CcsFacade
    {
        public static IDictionary<string, CcsProcess> ParseAll(string text){
			var lexer = new CcsLexer(text.ToAntlrInputStream());
			var parser = new CcsParser(lexer.GetTokenStream());
			var programCtx = parser.program();

			var listener = new CcsListener();
            listener.WalkContext(programCtx);
            return listener.GetProcessesTable();
        }

        public static IEnumerable<string> RandomTrace(CcsProcess start, int moves, bool printTau = false)
		{
            var result = new List<string>();
			var rand = new Random();
            IProcess proc = start;
			for (int i = 0; i < moves; i++)
			{
				var transitions = proc.GetTransitions();
				if (!transitions.Any())
				{
					break;
				}
				int index = rand.Next(0, transitions.Count());
				var nextTransition = transitions.ElementAt(index);
				proc = nextTransition.Process;
				if (nextTransition.Label != Const.tau || printTau)
				{
                    result.Add($"{i:000}: {nextTransition.Label}");
				}
			}
            return result;
		}
    }
}
