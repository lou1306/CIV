﻿using System.Runtime.CompilerServices;
using CIV.Common;

[assembly: InternalsVisibleTo("CIV.Test")]
namespace CIV.Hml
{
    public static class HmlFacade
    {
        public static HmlFormula ParseAll(string text)
        {
			var lexer = new HmlLexer(text.ToAntlrInputStream());
            var parser = new HmlParser(lexer.GetTokenStream());

            var formulaCtx = parser.baseHml();
            var listener = new HmlListener();
            listener.WalkContext(formulaCtx);
            return listener.RootFormula;
		}
    }
}
