using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.ANTLR.AST.Node
{
    abstract class ASTDecNode : ASTNode { }

    class DecsNode : ASTDecNode
    {
        public List<ASTDecNode> Decs { get; }
        public DecsNode(List<ASTDecNode> decs)
        {
            this.Decs = decs;
        }

        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "DecsNode {");
            foreach (var dec in this.Decs)
            {
                dec.printNode(tab + "\t");
            }

            Console.WriteLine(tab + "}");
        }
    }
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
        public Option Type { get; }
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

        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "VarDecNode {");
            Console.WriteLine(tab + "\tName: " + this.NameSymbol);
            if (this.Type != null)
            {
                Console.WriteLine(tab + "\tType: " + this.Type.Symbol);
            }

            if (this.Init != null)
            {
                Console.WriteLine(tab + "\tInit: ");
                this.Init.printNode(tab + "\t\t");
            }
            else
            {
                Console.WriteLine(tab + "\tInit: null");
            }

            Console.WriteLine(tab + "}");
        }
    }


    class FuncDecNode : ASTDecNode
    {
        public List<FuncDec> FuncDecs { get; }
        public FuncDecNode(List<FuncDec> funcDecs)
        {
            this.FuncDecs = funcDecs;
        }

        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "FuncDecNode {");

            foreach (var funcDec in FuncDecs)
            {
                Console.WriteLine(tab + "\tFunction:");
                funcDec.printNode(tab + "\t\t");
            }

            Console.WriteLine(tab + "}");
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
        public List<TypeSubClass> TypeSubs { get; }

        public TypeDecNode(List<TypeSubClass> typeSubs)
        {
            this.TypeSubs = typeSubs;
        }

        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "TypeDecNode {");
            foreach (var typeSub in TypeSubs)
            {
                Console.WriteLine(tab + "\tName: " + typeSub.NameSymbol);
                Console.WriteLine(tab + "\tType: ");
                typeSub.Ty.printNode(tab + "\t\t");  
            }

            Console.WriteLine(tab + "}");
        }

    }

}
