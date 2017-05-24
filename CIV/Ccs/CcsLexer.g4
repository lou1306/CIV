lexer grammar CcsLexer;

TERM: ';';

NIL: '0';
COMMA: ',';
DIV : '/' ;
DEF: '=';
PAR: '|';
PREFIX: '.';
CHOICE: '+';
TAU: 'tau';
LBRACE : '{' ;
RBRACE : '}' ;
MUL : '*' ;
SETDEF  : 'set ' ;
LPAREN : '(' ;
RPAREN : ')' ;
LBRACK : '[' ;
RBRACK : ']' ;
T__1 : '\\' ;


COACTION: '\''[a-z][A-Za-z0-9]*;
ACTION: [a-z][A-Za-z0-9]*;

fragment InputCharacter: ~[\r\n\u0085\u2028\u2029];
COMMENT
    : MUL InputCharacter+
    | MUL
    ;

IDENTIFIER
    : [A-Z][A-Za-z0-9]*
    ;

//RENAMINGS   : RENAMING ',' RENAMINGS;
//RENAMING    : ACTION  ACTION;

// ignore whitespace
WHITESPACE  : [ \r\n\t] + -> channel (HIDDEN);

