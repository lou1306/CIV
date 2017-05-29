using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using CIV.Interfaces;

[assembly: InternalsVisibleTo("CIV.Test")]
namespace CIV.Ccs
{
    public static class CcsFacade
    {
        public static IDictionary<string, IProcess> ParseAll(string text){
            var stream = new AntlrInputStream(text);
			var lexer = new CcsLexer(stream);
			var tokens = new CommonTokenStream(lexer);
			var parser = new CcsParser(tokens);
			var programCtx = parser.program();

			var listener = new CcsListener();
			ParseTreeWalker.Default.Walk(listener, programCtx);
            return listener.GetProcessesTable();
        }

        public static IEnumerable<string> RandomTrace(IProcess start, int moves, bool printTau = false)
		{
            var result = new List<string>();
			var rand = new Random();
			for (int i = 0; i < moves; i++)
			{
				var transitions = start.Transitions();
				if (!transitions.Any())
				{
					break;
				}
				int index = rand.Next(0, transitions.Count());
				var nextTransition = transitions.ElementAt(index);
				start = nextTransition.Process;
				if (nextTransition.Label != Const.tau || printTau)
				{
                    result.Add(String.Format("{0:000}: {1}", i, nextTransition.Label));
				}
			}
            return result;
		}
    }
}
