parser grammar HmlParser;

options {
    tokenVocab = HmlLexer;
}

labelList
    : label
    | labelList COMMA label
    ;

label : TAU | ACTION | COACTION ;

hml
    : LPAREN hml RPAREN                     #Parenth
    | NOT hml                               #Negated
    | hml AND hml                           #Conj
    | hml OR hml                            #Disj
    | LBOX labelList RBOX hml  #Box
    | LBOX ALL RBOX hml                     #BoxAll
    | LDIAMOND labelList RDIAMOND hml       #Diamond
    | LDIAMOND ALL RDIAMOND hml             #DiamondAll
    | LWBOX labelList RWBOX hml             #WeakBox
    | LWBOX ALL RWBOX hml                   #WeakBoxAll
    | LWDIAMOND labelList RWDIAMOND hml     #WeakDiamond
    | LWDIAMOND ALL RWDIAMOND hml           #WeakDiamondAll
    | TRUE                                  #True
    | FALSE                                 #False
    ;