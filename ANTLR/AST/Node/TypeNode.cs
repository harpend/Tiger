using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.ANTLR.AST.Node
{
    abstract class ASTTypeNode : ASTNode { }
    class NameTypeNode : ASTTypeNode
    {
        public string Symbol { get; }
        public int Pos { get; }
        public NameTypeNode(string symbol, int pos)
        {
            this.Symbol = symbol;
            this.Pos = pos;
        }

        public override void printNode(string tab)
        {
            Console.WriteLine(tab+"NameType{");
            Console.WriteLine(tab+"\tSymbol: "+ Symbol);
            Console.WriteLine(tab+"}");
        }
    }
    class ArrayTypeNode : ASTTypeNode
    {
        public string Symbol { get; }
        public int Pos { get; }
        public ArrayTypeNode(string symbol, int pos)
        {
            this.Symbol = symbol;
            this.Pos = pos;
        }

        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "NameType{");
            Console.WriteLine(tab + "\tSymbol: " + Symbol);
            Console.WriteLine(tab + "}");
        }
    }

    class RecordTypeNode : ASTTypeNode
    {
        public List<Field> Fields { get; }
        public RecordTypeNode(List<Field> fields)
        {
            this.Fields = fields;
        }

        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "RecordType{");
            Console.WriteLine(tab + "\tFields[");
            foreach (Field f in  Fields)
            {
                f.printNode(tab+"\t\t");

            }
            Console.WriteLine(tab + "\t]");
            Console.WriteLine(tab + "}");
        }
    }

}
