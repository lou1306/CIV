﻿using static System.Console;
using CIV.Formats;

namespace CIV
{
    class Program
    {
        static void Main(string[] args)
        {
  			var text = System.IO.File.ReadAllText(args[0]);

			//var processes = CcsFacade.ParseAll(text);
			//var hmlText = "[[ack]][[ack]][[ack]](<<ack>>tt and [[freeAll]]ff)";
			//var prova = HmlFacade.ParseAll(hmlText);
			//WriteLine(prova.Check(processes["Prison"]));

            var project = new Caal().Load(args[0]);
            WriteLine("Loaded project {0}", project.Name);
			
            foreach (var kv in project.Formulae)
            {
				WriteLine($"---->{kv.Key} {kv.Value}");

			    var isSatisfied = kv.Key.Check(kv.Value);
                var symbol = isSatisfied ? "|=" : "|/=";
                ForegroundColor = isSatisfied ? System.ConsoleColor.Green : System.ConsoleColor.Red;
                WriteLine($"{kv.Value} {symbol} {kv.Key}");
            }
            ResetColor();

		}
    }
}
