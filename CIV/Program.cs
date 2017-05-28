using CIV.Ccs;

namespace CIV
{
    class Program
    {
        static void Main(string[] args)
        {
			// Parse the file and do a random trace
            var processes = CcsFacade.ParseFile(args[0]);
			CcsFacade.RandomTrace(processes["Prison"], 450);
		}
    }
}
