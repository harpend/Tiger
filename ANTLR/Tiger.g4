﻿grammar Tiger;

// non-terminals
program: (expr | decs)+ EOF;

// done
decs: dec+;

dec: tydec # DecTyDec
	| vardec # DecVarDec
	| fundec # DecFunDec
	| IMPORT STRLIT # DecImport
	;

tydec: TYPE ID EQ ty;

// done
ty: typeid # TyTypeId
	| LBRACE tyfields RBRACE # TyBraced
	| ARRAY OF typeid # TyArray
	;

// done
tyfields: (field (COMMA field)*)?;

// done
field: ID COLON typeid;

typeid: ID;

vardec: VAR ID ASGN expr # SimpleVarDec
		| VAR ID COLON typeid ASGN expr # TypeVarDec
		;

fundec: FUNCTION ID LPAREN tyfields RPAREN EQ expr # SimpleFuncDec
		| FUNCTION ID LPAREN tyfields RPAREN COLON typeid EQ expr # TypeFuncDec
		;

lvalue: ID
		| lvalue DOT ID
		| lvalue LBRACKET expr RBRACKET;

// done
exprs: (expr (SC expr)*)?;

// done
expr: lvalue	#LeftVal
	  | NIL		#Nil
	  | INTLIT	#IntegerLiteral
	  | STRLIT	#StringLiteral
	  | ID LPAREN (expr (COMMA expr)*)? RPAREN  # FunctionCall
	  | typeid LBRACE (ID EQ expr (COMMA ID EQ expr)*)? RBRACE # RecordCreation
	  | typeid LBRACKET expr RBRACKET OF expr		# ArrayCreation
      | MINUS expr                                 # NegationExpr
      | expr MULT expr                            # MultExpr
      | expr DIV expr                            # DivExpr
      | expr PLUS expr                            # AddExpr
      | expr MINUS expr                            # SubExpr
	  | expr NE expr							# NeqExpr
      | expr LT expr                            # LtExpr
      | expr GT expr                            # GtExpr
      | expr LE expr                           # LeExpr
      | expr GE expr                           # GeExpr
      | expr EQ expr                           # EqExpr
      | expr AND expr                           # AndExpr
      | expr OR expr						     # OrExpr
	  | LPAREN expr RPAREN						# ParenNestExpr
	  | lvalue ASGN expr						# Assignment
	  | IF expr THEN expr (ELSE expr)?			# IfExpr
	  | WHILE expr DO expr						# WhileExpr
	  | FOR ID ASGN expr TO expr DO expr		# ForExpr
	  | BREAK									# BreakExpr
	  |	LET decs IN exprs END					# LetExpr
	  ;


// terminals
COMMENT: '//' ~[\r\n]* '\n' -> skip;
IMPORT: 'import';
INT: 'int';
STRING: 'string';
WHILE: 'while';
FOR: 'for';
TO: 'to';
BREAK: 'break';
LET: 'let';
IN: 'in';
END: 'end';
FUNCTION: 'fn';
VAR: 'var';
TYPE: 'type';
ARRAY: 'array';
IF: 'if';
THEN: 'then';
ELSE: 'else';
DO: 'do';
OF: 'of';
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
NE: '!=';
AND: '&&';
OR: '||';
ASGN: ':=';
COMMA: ',';
COLON: ':';
SC: ';';
LPAREN: '(';
RPAREN: ')';
LBRACKET: '[';
RBRACKET: ']';
LBRACE: '{';
RBRACE: '}';
DOT: '.';
 
ID: [a-zA-Z][a-zA-Z0-9_]*;
INTLIT: [0-9]+;
STRLIT: '"' (~["\\] | '\\' . )* '"';
WS: [ \t\r\n]+ -> skip;


