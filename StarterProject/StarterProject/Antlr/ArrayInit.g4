grammar ArrayInit;  //Must match file name

//Parser rules start with lowercase

init    : '{' value (',' value)* '}' ;

value   : init
        | INT ;

//Lexer Rules start with upper case

INT     : [0-9]+;
WS      : [ \t\r\n]+ -> skip;
