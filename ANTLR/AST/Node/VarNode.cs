using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.ANTLR.AST.Node
{
    abstract class ASTVarNode : ASTNode
    {
        public int Pos { get; }

        protected ASTVarNode(int pos)
        {
            this.Pos = pos;
        }
    }

    class SimpleVarNode : ASTVarNode
    {
        public string Symbol { get; }
        public SimpleVarNode(string symbol, int pos) : base(pos)
        {
            this.Symbol = symbol;
        }
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "SimpleVar{");
            Console.WriteLine(tab + "\tSymbol: ", Symbol);
            Console.WriteLine(tab + "}");
        }
    }

    class FieldVarNode : ASTVarNode
    {
        public ASTVarNode Var { get; }
        public string Symbol { get; }

        public FieldVarNode(ASTVarNode var, string symbol, int pos) : base(pos)
        {
            this.Symbol = symbol;
            this.Var = var;
        }

        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "SimpleVar{");
            Console.WriteLine(tab + "\tSymbol: ", Symbol);
            Console.WriteLine(tab + "\tVar: ");
            this.Var.printNode(tab + "\t\t");
            Console.WriteLine(tab + "}");
        }
    }

    class SubscriptVarNode : ASTVarNode
    {
        public ASTVarNode Var { get; }
        public ASTExprNode Expr { get; }

        public SubscriptVarNode(ASTVarNode var, ASTExprNode expr, int pos) : base(pos)
        {
            this.Expr = expr;
            this.Var = var;
        }
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "SimpleVar{");
            Console.WriteLine(tab + "\tExpr: ");
            this.Expr.printNode(tab + "\t\t");
            Console.WriteLine(tab + "\tVar: ");
            this.Var.printNode(tab + "\t\t");
            Console.WriteLine(tab + "}");
        }
    }
}
