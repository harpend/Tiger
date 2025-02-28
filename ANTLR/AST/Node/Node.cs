using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.ANTLR.AST.Node
{
    abstract class ASTNode { }
    class Field
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
   }
    class FuncDec
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
    }

}
