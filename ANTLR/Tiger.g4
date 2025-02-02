grammar g4;

program: line* EOF;

line: statement | ifStmt | whileStmt;

statement: (assignment|function) ';';

ifStmt: 'if' '(' expression ')' block ('else' elseIfStmt)?;

elseIfStmt: block | ifStmt;

block: '{' line* '}';

whileStmt: WHILE '(' expression ')' block;

WS: [ \t\r\n]+ -> skip;


