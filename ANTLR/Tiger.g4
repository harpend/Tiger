grammar Tiger;

// non-terminals
program: line* EOF;

line: statement | ifStmt | whileStmt;

statement: (assignment|functionCall) ';';

ifStmt: 'if' '(' expression ')' block ('else' elseIfStmt)?;

elseIfStmt: block | ifStmt;

block: '{' line* '}';

whileStmt: WHILE '(' expression ')' block;

assignment: type IDENTIFIER ASGN expression;

type: INT | STRING;

functionCall: IDENTIFIER '(' (expression (',' expression)*)? ')';

expression: constant
	| IDENTIFIER
	| functionCall
	| '(' expression ')'
	| expression mathOp expression
	| expression boolOp expression;

mathOp: PLUS | MINUS | DIV | MULT;
boolOp: LT | GT | LE | GE | EQ | AND | OR;
constant: INTLIT | STRLIT;

// terminals
COMMENT: '//' ~[\r\n]* '\n' -> skip;
INT: 'int';
STRING: 'string';
WHILE: 'while';
FOR: 'for';
BREAK: 'break';
ARRAY: 'array';
IN: 'in';
FUNCTION: 'fn';
IF: 'if';
ELSE: 'else';
DO: 'do';
NIL: 'nil';
PLUS: '+';
MINUS: '-';
DIV: '/';
MULT: '*';
LT: '<';
GT: '>';
LE: '<=';
GE: '>=';
EQ: '=';
AND: '&&';
OR: '||';
ASGN: ':=';
IDENTIFIER: [a-zA-Z][a-zA-Z0-9_]*;
INTLIT: [0-9]+;
STRLIT: '"' (~["\\] | '\\' . )* '"';
WS: [ \t\r\n]+ -> skip;


