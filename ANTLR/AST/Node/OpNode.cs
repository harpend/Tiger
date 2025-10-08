using Antlr4.Runtime.Atn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Translate;
using Tiger.Tree;
using static Tiger.Translate.Ex;

namespace Tiger.ANTLR.AST.Node
{
    public abstract class ASTOpNode : ASTNode {
        public abstract IExpr TransOp(Tree.Expr left, Tree.Expr right);
    }
    public class PlusOpNode : ASTOpNode {
        public override void printNode(string tab)
        {
            Console.WriteLine(tab+ "+");
        }

        public override IExpr TransOp(Tree.Expr left, Tree.Expr right)
        {
            return new Ex(new BinOpExpr(BinOp.PLUS, left, right));
        }
    }
    public class MinusOpNode : ASTOpNode
    {
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "-");
        }

        public override IExpr TransOp(Tree.Expr left, Tree.Expr right)
        {
            return new Ex(new BinOpExpr(BinOp.MINUS, left, right));
        }
    }
    public class TimesOpNode : ASTOpNode {
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "*");
        }

        public override IExpr TransOp(Tree.Expr left, Tree.Expr right)
        {
            return new Ex(new BinOpExpr(BinOp.MUL, left, right));
        }
    }
    public class DivideOpNode : ASTOpNode {
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "/");
        }

        public override IExpr TransOp(Tree.Expr left, Tree.Expr right)
        {
            return  new Ex(new BinOpExpr(BinOp.DIV, left, right));
        }
    }
    public class EqOpNode : ASTOpNode
    {
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "=");
        }


        public override IExpr TransOp(Tree.Expr left, Tree.Expr right)
        {
            return new Cx((Frame.Temp.Label l1, Frame.Temp.Label l2) => new CJump(RelOp.EQ, left, right, l1, l2));
        }
    }
    public class NeqOpNode : ASTOpNode
    { 
        public override void printNode(string tab)
        {
            Console.WriteLine(tab+ "!=");
        }

        public override IExpr TransOp(Tree.Expr left, Tree.Expr right)
        {
            return new Cx((Frame.Temp.Label l1, Frame.Temp.Label l2) => new CJump(RelOp.NE, left, right, l1, l2));
        }
    }
    public class LtOpNode : ASTOpNode
    { 
    public override void printNode(string tab)
        {
            Console.WriteLine(tab+ "<");
        }

        public override IExpr TransOp(Tree.Expr left, Tree.Expr right)
        {
            return new Cx((Frame.Temp.Label l1, Frame.Temp.Label l2) => new CJump(RelOp.LT, left, right, l1, l2));
        }
    }
    public class LeOpNode : ASTOpNode
    { 
    public override void printNode(string tab)
        {
            Console.WriteLine(tab+ "<=");
        }

        public override IExpr TransOp(Tree.Expr left, Tree.Expr right)
        {
            return new Cx((Frame.Temp.Label l1, Frame.Temp.Label l2) => new CJump(RelOp.LE, left, right, l1, l2));
        }
    }
    public class GtOpNode : ASTOpNode
    { 
    public override void printNode(string tab)
        {
            Console.WriteLine(tab+ ">");
        }

        public override IExpr TransOp(Tree.Expr left, Tree.Expr right)
        {
            return new Cx((Frame.Temp.Label l1, Frame.Temp.Label l2) => new CJump(RelOp.GT, left, right, l1, l2));
        }
    }
    public class GeOpNode : ASTOpNode
    { 
    public override void printNode(string tab)
        {
            Console.WriteLine(tab+ ">=");
        }
        public override IExpr TransOp(Tree.Expr left, Tree.Expr right)
        {
            return new Cx((Frame.Temp.Label l1, Frame.Temp.Label l2) => new CJump(RelOp.GE, left, right, l1, l2));
        }
    }
}
