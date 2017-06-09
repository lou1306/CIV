using System;
using System.Diagnostics;
using CIV.Formats;
using CIV.Common;
using static System.Console;

namespace CIV
{

	[Flags]
	enum ExitCode
	{
		Success = 0,
        UnknownError = 1,
		FileNotFound = 2,
        ParsingFailed = 4,
        VerificationFailed = 8,
	}


    class Program
    {
        static void Main(string[] args)
        {
            try
            {
				var project = new Caal().Load(args[0]);
                var result = VerifyAll(project);
                if (!result)
                {
                    Environment.Exit((int)ExitCode.VerificationFailed);
                }
            }
            catch (Exception ex)
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("An error has occurred:");
                WriteLine(ex.Message);
				ResetColor();

                ExitCode exitCode;
                switch (ex)
                {
                    case System.IO.FileNotFoundException e:
                        exitCode = ExitCode.FileNotFound;
                        break;
                    case ParsingFailedException e:
                        exitCode = ExitCode.ParsingFailed;
                        break;
                    default:
                        exitCode = ExitCode.UnknownError;
                        break;
                }
                Environment.Exit((int)exitCode);
			}
		}

        static bool VerifyAll(Caal project)
        {
			WriteLine("Loaded project {0}. Starting verification...", project.Name);

            var sw = new Stopwatch();
            sw.Start();

            bool result = true;
			foreach (var kv in project.Formulae)
			{
                Write($"{kv.Value} |= {kv.Key}...");
				Out.Flush();
				var isSatisfied = kv.Key.Check(kv.Value);
                result &= isSatisfied;
				ForegroundColor = isSatisfied ? ConsoleColor.Green : ConsoleColor.Red;
                Write(isSatisfied ? "Success!" : "Failure");
                WriteLine();
				ResetColor();
			}
            sw.Stop();
            WriteLine($"Completed in {sw.Elapsed.TotalMilliseconds} ms.");
            return result;
        }
    }
}
