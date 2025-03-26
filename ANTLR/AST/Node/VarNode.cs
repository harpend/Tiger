using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Table;

namespace Tiger.ANTLR.AST.Node
{
    abstract class ASTVarNode : ASTNode
    {
        public int Pos { get; }

        protected ASTVarNode(int pos)
        {
            this.Pos = pos;
        }

        public abstract TigerType CheckType(SymbolTable symbolTable, TypeTable typeTable);
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

        public override TigerType CheckType(SymbolTable symbolTable, TypeTable typeTable) // not one type for this, therefore it should never be checked or maybe return the first type.
        {
            Symbol s = symbolTable.Get(this.Symbol);
            if (Symbol != null)
            {
                return s.type;
            }

            throw new Exception(Error.TypeError.NonExistantType());
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
            Console.WriteLine(tab + "FieldVar{");
            Console.WriteLine(tab + "\tSymbol: ", Symbol);
            Console.WriteLine(tab + "\tVar: ");
            this.Var.printNode(tab + "\t\t");
            Console.WriteLine(tab + "}");
        }

        public override TigerType CheckType(SymbolTable symbolTable, TypeTable typeTable) // not one type for this, therefore it should never be checked or maybe return the first type.
        {
            TigerType tt = this.Var.CheckType(symbolTable, typeTable);
            TigerType ftt = tt.fields[this.Symbol];
            if (ftt == null) throw new Exception(Error.TypeError.NonExistantType());
            return ftt;
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
            Console.WriteLine(tab + "SubscriptVar{");
            Console.WriteLine(tab + "\tExpr: ");
            this.Expr.printNode(tab + "\t\t");
            Console.WriteLine(tab + "\tVar: ");
            this.Var.printNode(tab + "\t\t");
            Console.WriteLine(tab + "}");
        }
        public override TigerType CheckType(SymbolTable symbolTable, TypeTable typeTable) // not one type for this, therefore it should never be checked or maybe return the first type.
        {
            TigerType tt = this.Var.CheckType(symbolTable, typeTable);
            TigerType ett = this.Expr.CheckType(symbolTable, typeTable);
            if (ett != tt) throw new Exception(Error.TypeError.NonExistantType());
            return tt;
        }
    }
}
