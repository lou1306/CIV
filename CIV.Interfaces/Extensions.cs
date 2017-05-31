using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace CIV.Interfaces
{
    public static class Extensions
    {

        public static AntlrInputStream ToAntlrInputStream(this string text)
        {
            return new AntlrInputStream(text);
        }
        public static CommonTokenStream GetTokenStream(this Lexer lexer)
		{
            return new CommonTokenStream(lexer);
		}
        public static void WalkContext(
            this IParseTreeListener listener,
            ParserRuleContext context)
        {
			ParseTreeWalker.Default.Walk(listener, context);
		}

    }
}
