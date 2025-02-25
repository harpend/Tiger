using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.ANTLR.AST.Node
{
    abstract class ASTDecNode : ASTNode { }

    class VarDecNode : ASTDecNode
    {
        public bool BoolRef { get; } // escape
        public class Option
        {
            public string Symbol { get; }
            public int Pos { get; }
            public Option(string symbol, int pos)
            {
                this.Symbol = symbol;
                this.Pos = pos;
            }
        }
        public Option Type { get; } // not technically a simple var node just shares same fields
        public ASTExprNode Init { get; }
        public int Pos { get; }
        public string NameSymbol { get; }
        public VarDecNode(bool boolRef, Option type, ASTExprNode init, int pos, string symbol)
        {
            this.BoolRef = boolRef;
            this.Type = type;
            this.Init = init;
            this.Pos = pos;
            this.NameSymbol = symbol;
        }
    }


    class FuncDecNode : ASTDecNode
    {
        public FuncDec[] FuncDecs { get; }
        public FuncDecNode(FuncDec[] funcDecs)
        {
            this.FuncDecs = funcDecs;
        }
    }

    class TypeDecNode : ASTDecNode
    {
        public class TypeSubClass
        {
            public string NameSymbol { get; }
            public ASTTypeNode Ty { get; }
            public int Pos { get; }
            public TypeSubClass(string nameSymbol, ASTTypeNode ty, int pos)
            {
                NameSymbol = nameSymbol;
                Ty = ty;
                Pos = pos;
            }
        }
        public TypeSubClass[] TypeSubs { get; }

        public TypeDecNode(TypeSubClass[] typeSubs)
        {
            this.TypeSubs = typeSubs;
        }
    }

}
