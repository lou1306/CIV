using System;
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
			WriteLine("Loaded project {0}", project.Name);

			foreach (var kv in project.Formulae)
			{
				var isSatisfied = kv.Key.Check(kv.Value);
				var symbol = isSatisfied ? "|=" : "|/=";
				ForegroundColor = isSatisfied ? ConsoleColor.Green : ConsoleColor.Red;
				WriteLine($"{kv.Value} {symbol} {kv.Key}");
			}
			ResetColor();
        }
    }
}
