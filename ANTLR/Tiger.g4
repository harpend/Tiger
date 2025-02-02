grammar tiger;

program: line* EOF;

line: statement | ifStmt | whileStmt;

statement: (assignment|functionCall) ';';

ifStmt: 'if' '(' expression ')' block ('else' elseIfStmt)?;

elseIfStmt: block | ifStmt;

block: '{' line* '}';

whileStmt: WHILE '(' expression ')' block;

assignment: type IDENTIFIER ':=' expression;

type: INT | STRING;

functionCall: 'fn' type IDENTIFIER '(' (expression (',' expression)*)? ')';

expression: constant
	| IDENTIFIER
	| functionCall
	| '(' expression ')'
	| expression op expression

// terminals
INT: 'int';
STRING: 'string';
WHILE: 'while';
IDENTIFIER: [a-zA-Z][a-zA-Z0-9_]*;
WS: [ \t\r\n]+ -> skip;


