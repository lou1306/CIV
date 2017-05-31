using System;
namespace CIV.Ccs
{
    public static class Const
    {
        public static readonly string tau = GetLiteral(CcsLexer.TAU);
        public static readonly string nil = GetLiteral(CcsLexer.NIL);
		public static readonly string par = GetLiteral(CcsLexer.PAR);
		public static readonly string prefix = GetLiteral(CcsLexer.PREFIX);
        public static readonly string choice = GetLiteral(CcsLexer.CHOICE);
        public static readonly string restrictFormat =
            String.Format(
                "{{0}}{0}{1}{{1}}{2}",
                GetLiteral(CcsLexer.T__1),
                GetLiteral(CcsLexer.LBRACE),
                GetLiteral(CcsLexer.RBRACE));
        public static readonly string relabelFormat =
			String.Format(
				"{{0}}{0}{{1}}{1}",
                GetLiteral(CcsLexer.LBRACK),
				GetLiteral(CcsLexer.RBRACK));

        static string GetLiteral(int id)
        {
            return CcsLexer.DefaultVocabulary
                           .GetLiteralName(id)
                           .Replace("'", "");
        }
    }
}
