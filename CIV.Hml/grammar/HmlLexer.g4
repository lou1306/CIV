lexer grammar HmlLexer;

TERM: ';';

TRUE        : 'tt';
FALSE       : 'ff';
AND         : 'and';
OR          : 'or';
NOT         : 'not';
LPAREN      : '(';
RPAREN      : ')';
LBOX        : '[';
RBOX        : ']';
LWBOX       : '[[';
RWBOX       : ']]';
LDIAMOND    : '<';
RDIAMOND    : '>';
LWDIAMOND   : '<<';
RWDIAMOND   : '>>';
ALL         : '-';
COMMA       : ',';

TAU : 'tau';
COACTION: '\''[a-z][A-Za-z0-9]*;
ACTION: [a-z][A-Za-z0-9]*;

MAXFP : 'max=';
MINFP : 'min=';

WHITESPACE  : [ \r\n\t] + -> channel (HIDDEN);


IDENTIFIER
    : [A-Z][A-Za-z0-9]*
    ;
