parser grammar HmlParser;

options {
    tokenVocab = HmlLexer;
}

labelList
    : label
    | labelList COMMA label
    ;

label : TAU | ACTION | COACTION ;

baseHml : hml TERM;

hml
    : LPAREN hml RPAREN                                 #Parenth
    | NOT hml                                           #Negated
    | <assoc=right>LBOX labelList RBOX hml              #Box
    | <assoc=right>LBOX ALL RBOX hml                    #BoxAll
    | <assoc=right>LDIAMOND labelList RDIAMOND hml      #Diamond
    | <assoc=right>LDIAMOND ALL RDIAMOND hml            #DiamondAll
    | <assoc=right>LWBOX labelList RWBOX hml            #WeakBox
    | <assoc=right>LWBOX ALL RWBOX hml                  #WeakBoxAll
    | <assoc=right>LWDIAMOND labelList RWDIAMOND hml    #WeakDiamond
    | <assoc=right>LWDIAMOND ALL RWDIAMOND hml          #WeakDiamondAll
    | hml AND hml                                       #Conj
    | hml OR hml                                        #Disj
    | TRUE                                              #True
    | FALSE                                             #False
    ;