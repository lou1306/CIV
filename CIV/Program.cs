using CIV.Ccs;

namespace CIV
{
    class Program
    {
        static void Main(string[] args)
        {
  			var text = System.IO.File.ReadAllText(args[0]);

			var processes = CcsFacade.ParseAll(text);
            var trace = CcsFacade.RandomTrace(processes["Prison"], 450);
            foreach (var action in trace)
            {
                System.Console.WriteLine(action);
            }
        }
    }
}
