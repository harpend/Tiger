using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Tree
{
    public enum BinOp
    {
        PLUS, MINUS, MUL, DIV,
        AND, OR, LSHIFT, RSHIFT, ARSHIFT, XOR
    }

    public enum RelOp
    {
        EQ, NE, LT, GT, LE, GE,
        ULT, ULE, UGT, UGE
    }

    // TODO: handle expr lists, maybe special ir member?
    public abstract class Stmt
    {

    }

    public abstract class Expr : Stmt
    {

    }

    public class Const : Expr
    {
        public int c;
        public Const(int c)
        {
            this.c = c;
        }
    }

    public class Name : Expr
    {
        public Label label;
        public Name(Label label)
        {
            this.label = label;
        }
    }

    public class Temp : Expr
    {
        public Frame.Temp.Temp temp;
        public Temp(Frame.Temp.Temp temp)
        {
            this.temp = temp;
        }
    }

    public class BinOpExpr : Expr
    {
        public BinOp bo;
        public Expr expr1;
        public Expr expr2;
        public BinOpExpr(BinOp bo, Expr expr1, Expr expr2)
        {
            this.bo = bo;
            this.expr1 = expr1;
            this.expr2 = expr2;
        }
    }

    public class Mem : Expr
    {
        public Expr expr;
        public Mem(Expr expr)
        {
            this.expr = expr;
        }
    }

    public class Call : Expr
    {
        public Expr expr;
        public LinkedList<Expr> exprList;
        public Call(LinkedList<Expr> exprList, Expr expr)
        {
            this.exprList = exprList;
            this.expr = expr;
        }
    }

    public class Eseq : Expr
    {
        public Stmt stmt;
        public Expr expr;
        public Eseq(Stmt stmt, Expr expr)
        {
            this.stmt = stmt;
            this.expr = expr;
        }
    }

    public class Move : Stmt
    {
        public Expr expr1;
        public Expr expr2;
        public Move(Expr expr1, Expr expr2)
        {
            this.expr1 = expr1;
            this.expr2 = expr2;
        }
    }

    public class ExprStmt : Stmt
    {
        public Expr expr;
        public ExprStmt(Expr expr)
        {
            this.expr = expr;
        }
    }

    public class Jump : Stmt
    {
        public Expr expr;
        public LinkedList<Temp> tempList;
        public Jump(Expr expr, LinkedList<Temp> tempList)
        {
            this.expr = expr;
            this.tempList = tempList;
        }
    }

    public class CJump : Stmt
    {
        public RelOp relop;
        public Expr expr1;
        public Expr expr2;
        public Frame.Temp.Label label1;
        public Frame.Temp.Label label2;
        public CJump(RelOp relop, Expr expr1, Expr expr2, Frame.Temp.Label label1, Frame.Temp.Label label2)
        {
            this.relop = relop;
            this.expr1 = expr1;
            this.expr2 = expr2;
            this.label1 = label1;
            this.label2 = label2;
        }
    }

    public class Seq : Stmt
    {
        public Stmt stmt1;
        public Stmt stmt2;
        public Seq(Stmt stmt1, Stmt stmt2)
        {
            this.stmt1 = stmt1;
            this.stmt2 = stmt2;


        }
    }

    public class Label : Stmt
    {
        public Frame.Temp.Label label;
        public Label(Frame.Temp.Label label)
        {
            this.label = label;
        }
    }
}
