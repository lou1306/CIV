using System;
using static CIV.Ccs.CcsLexer;
namespace CIV.Ccs
{
    public static class Const
    {
        public static readonly string tau = GetLiteral(TAU);
        public static readonly string nil = GetLiteral(NIL);
        public static readonly string par = GetLiteral(PAR);
        public static readonly string prefix = GetLiteral(PREFIX);
        public static readonly string choice = GetLiteral(CHOICE);
        public static readonly string relab = GetLiteral(DIV);
        public static readonly string restrictFormat =
            $"{{0}}{GetLiteral(DIV)}{GetLiteral(LBRACE)}{{1}}{GetLiteral(RBRACE)}";
        public static readonly string relabelFormat =
            $"{{0}}{GetLiteral(LBRACK)}{{1}}{GetLiteral(RBRACK)}";

        static string GetLiteral(int id)
        {
            return DefaultVocabulary
                .GetLiteralName(id)
                .Replace("'", "")
                .Replace("{", "{{")
                .Replace("}", "}}");
        }
    }
}
