grammar expr;
import CommonLexerRules;
// Parse Rules

prog    : stat+;

stat    : expr NEWLINE              #printExpr
        | ID '=' expr NEWLINE       #assign
        | NEWLINE                   #blank
        | 'clear'					#clear
        ;

expr    : expr op=('*'|'/') expr    #mulDiv
        | expr op=('+'|'-') expr    #addSub
        | INT                       #int
        | ID                        #id
        | '(' expr ')'              #parens
        ;

MUL : '*';
DIV : '/';
ADD : '+';
SUB : '-';
