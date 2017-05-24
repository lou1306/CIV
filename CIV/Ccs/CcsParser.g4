parser grammar CcsParser;

options {
    tokenVocab = CcsLexer;
}

program     : line*;

line        : statement
            | comment
            ;

statement   : procDef
            | setDef
            ;

comment     : COMMENT;

procDef     : IDENTIFIER DEF process TERM ;

setDef      : SETDEF IDENTIFIER DEF setExpression TERM ;

process     :   LPAREN process RPAREN               #parenthProc
            |   process renamingExpression          #renamProc
            |   process T__1 setId                  #restrictIdProc
            |   process T__1 setExpression          #restrictExprProc
            |   <assoc=right>label PREFIX process   #prefixProc
            |   process PAR process                 #parProc
            |   process CHOICE process              #choiceProc
            |   pid                                 #pidProc
            |   NIL                                 #nilProc
            ;

label : TAU | ACTION | COACTION ;


pid         : IDENTIFIER
            ;


renamingExpression
    : LBRACK renamingList RBRACK
    ;

renamingList
    : renaming
    | renamingList COMMA renaming
    ;

renaming:
    action DIV nonTauAction;

setVar
    : setId
    | setExpression
    ;

setId : IDENTIFIER;
setExpression : LBRACE setList RBRACE;

setList
    : nonTauAction
    | setList COMMA nonTauAction
    ;

action
    : TAU
    | nonTauAction
    ;

nonTauAction : ACTION;