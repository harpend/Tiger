using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.ANTLR.AST.Node
{
    abstract class ASTNode {
        public abstract void printNode(string tab);
    }
    class Field : ASTNode
    {
        public string NameSymbol { get; }
        public bool BoolRef { get; }
        public string TypeSymbol { get; }
        public int Pos { get; }
        public Field(string nameSymbol, bool boolRef, string typeSymbol, int pos)
        {
            this.NameSymbol = nameSymbol;
            this.BoolRef = boolRef;
            this.TypeSymbol = typeSymbol;
            this.Pos = pos;
        }
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "Field {");
            Console.WriteLine(tab + "\tName: " + this.NameSymbol);
            Console.WriteLine(tab + "\tType: " + this.TypeSymbol);
            Console.WriteLine(tab + "}");
        }
   }
    class FuncDec : ASTNode
    {
        public string NameSymbol { get; }
        public List<Field> Fields { get; }
        public SimpleVarNode Option { get; }
        public ASTExprNode Body { get; }
        public int Pos { get; }
        public FuncDec(string nameSymbol, List<Field> fields, SimpleVarNode option, ASTExprNode body, int pos)
        {
            this.NameSymbol = nameSymbol;
            this.Fields = fields;
            this.Option = option;
            this.Body = body;
            this.Pos = pos;
        }
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "FuncDec {");
            Console.WriteLine(tab + "\tName: " + this.NameSymbol);
            Console.WriteLine(tab + "\tFields[ ");
            foreach (Field f in this.Fields)
            {
                f.printNode(tab + "\t\t");
            }
            Console.WriteLine(tab + "]");
            this.Body.printNode(tab + "\t\t");
            Console.WriteLine(tab + "}");
        }
    }

    class ProgramNode : ASTNode
    {
        public List<DecsNode> Decs { get; }
        public List<ASTExprNode> Expressions { get; }
        public ProgramNode(List<DecsNode> decs, List<ASTExprNode> expressions)
        {
            this.Decs = decs;
            this.Expressions = expressions;
        }
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "Program {");
            Console.WriteLine(tab + "\tDecs[ ");
            foreach (DecsNode ds in this.Decs)
            {
                ds.printNode(tab + "\t\t");
            }
            Console.WriteLine(tab+"]");
            Console.WriteLine(tab + "\tExprs[ ");
            foreach (ASTExprNode e in this.Expressions)
            {
                e.printNode(tab + "\t\t");
            }
            Console.WriteLine(tab+"]");
            Console.WriteLine(tab + "}");
        }
    }
}
