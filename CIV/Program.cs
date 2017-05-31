﻿using static System.Console;
using CIV.Ccs;
using CIV.Hml;

namespace CIV
{
    class Program
    {
        static void Main(string[] args)
        {
  			var text = System.IO.File.ReadAllText(args[0]);

			var processes = CcsFacade.ParseAll(text);
			var hmlText = "[[ack]][[ack]][[ack]](<<ack>>tt and [[freeAll]]ff)";
			var prova = HmlFacade.ParseAll(hmlText);
			WriteLine(prova.Check(processes["Prison"]));
		}
    }
}
