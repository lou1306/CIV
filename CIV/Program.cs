using System;
using System.Diagnostics;
using CIV.Formats;
using static System.Console;

namespace CIV
{

	[Flags]
	enum ExitCodes : int
	{
		Success = 0,
		FileNotFound = 1,
        ParsingFailed = 2,
        VerificationFailed = 4
	}


    class Program
    {
        static void Main(string[] args)
        {
            try
            {
				var project = new Caal().Load(args[0]);
                VerifyAll(project);
			}
            catch (System.IO.FileNotFoundException ex)
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine(ex.Message);
				ResetColor();
                Environment.Exit((int)ExitCodes.FileNotFound);
			}
		}

        static void VerifyAll(Caal project)
        {
			WriteLine("Loaded project {0}. Starting verification...", project.Name);

            var sw = new Stopwatch();
            sw.Start();
			foreach (var kv in project.Formulae)
			{
                Write($"{kv.Value} |= {kv.Key}...");
				Out.Flush();
				var isSatisfied = kv.Key.Check(kv.Value);
				ForegroundColor = isSatisfied ? ConsoleColor.Green : ConsoleColor.Red;
                var result = isSatisfied ? "Success!" : "Failure";
                Write($"\t{result}");
                WriteLine();
				ResetColor();
			}
            sw.Stop();
            WriteLine($"Completed in {sw.Elapsed.TotalMilliseconds} ms.");

        }
    }
}
