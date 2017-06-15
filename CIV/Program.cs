using System;
using System.Diagnostics;
using CIV.Formats;
using CIV.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
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
				var sw = new Stopwatch();
				sw.Start();
                var result = VerifyAll(project).GetAwaiter().GetResult();
				sw.Stop();
				WriteLine($"Completed in {sw.Elapsed.TotalMilliseconds} ms.");

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

        static async Task<bool> VerifyAll(Caal project)
        {
			WriteLine("Loaded project {0}. Starting verification...", project.Name);

            var tasks = new List<Task<bool>>();

			foreach (var kv in project.Formulae)
			{
                tasks.Add(Task.Run(() => Verify(kv.Value, kv.Key)));
			}
            await Task.WhenAll(tasks);
            return tasks.All(x => x.Result);
        }

        static bool Verify(IProcess process, Hml.HmlFormula property)
        {
			var isSatisfied = property.Check(process);
            WriteLine($"{process} |= {property}\t {isSatisfied}");
            return isSatisfied;
        }
    }
}
