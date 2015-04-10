lexer grammar CommonLexerRules;

ID      : [a-zA-Z]+; //match Identifiers
INT     : [0-9]+; //match integers
NEWLINE : '\r'? '\n'; //return newlines to parser
WS      : [ \t]+ -> skip; //toss out whitespace
