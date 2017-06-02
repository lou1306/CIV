﻿using static System.Console;
using CIV.Formats;
using CIV.Ccs;

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
			WriteLine("Processes:");
            //foreach (var kv in project.Processes)
            //{
            //    WriteLine("{0} = {1}", kv.Key, (kv.Value as PidProcess).Inner);
            //}
            foreach (var kv in project.Formulae)
            {
                WriteLine($"Result for formula {kv.Value}:");
                WriteLine(kv.Value.Check(project.Processes[kv.Key]));
            }
        }
    }
}
