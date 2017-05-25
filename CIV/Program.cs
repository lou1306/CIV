using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using CIV.Ccs;
using System.Linq;
using System.Text;
using CIV.Processes;

namespace CIV
{
    class Program
    {
        static void Main(string[] args)
        {
            // Parse the file
            var inputStream = Program.CreateInputStream("prisoners.ccs.txt");
            var lexer = new CcsLexer(inputStream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new CcsParser(tokens);
            var programCtx = parser.program();

            var listener = new CcsListener();
            ParseTreeWalker.Default.Walk(listener, programCtx);

            var factory = listener.GetProcessFactory();
            var prison = factory.Create(listener.Processes["Prison"]);
            RandomTrace(prison, 450);
        }

        static AntlrInputStream CreateInputStream(string filename)
        {
            var text = System.IO.File.ReadAllText(filename);
            return new AntlrInputStream(text);
        }

        static void RandomTrace(IProcess start, int moves, bool printTau = false)
        {
            var rand = new Random();
            for (int i = 0; i < moves; i++)
            {
                var transitions = start.Transitions();
                if (transitions.Count() == 0)
                {
                    break;
                }
                int index = rand.Next(0, transitions.Count());
                var nextTransition = transitions.ElementAt(index);
                start = nextTransition.Process;
                if (nextTransition.Label != "tau" | printTau)
                {
                    Console.WriteLine(
                        String.Format("{0:000}: {1}", i, nextTransition.Label));
                }
            }
        }
    }
}
