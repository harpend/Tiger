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
    }

    class RecordTypeNode : ASTTypeNode
    {
        public List<Field> Fields { get; }
        public RecordTypeNode(List<Field> fields)
        {
            this.Fields = fields;
        }
    }

}
