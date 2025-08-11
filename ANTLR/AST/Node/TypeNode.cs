using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Tiger.Semant;
using Tiger.Symbol;

namespace Tiger.ANTLR.AST.Node
{
    public abstract class ASTTypeNode : ASTNode {
        public abstract Type.Type CheckType(Env env);
    }
    public class NameTypeNode : ASTTypeNode
    {
        public string Sym { get; }
        public int Pos { get; }
        public NameTypeNode(string symbol, int pos)
        {
            this.Sym = symbol;
            this.Pos = pos;
        }

        public override void printNode(string tab)
        {
            Console.WriteLine(tab+"NameType{");
            Console.WriteLine(tab+"\tSymbol: "+ Sym);
            Console.WriteLine(tab+"}");
        }

        public override Type.Type CheckType(Env env)
        {
            return (Type.Type)env.typeEnv.Get(Symbol.Symbol.Intern(this.Sym));
        }
    }
    public class ArrayTypeNode : ASTTypeNode
    {
        public string Sym { get; }
        public int Pos { get; }
        public ArrayTypeNode(string symbol, int pos)
        {
            this.Sym = symbol;
            this.Pos = pos;
        }

        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "ArraryType{");
            Console.WriteLine(tab + "\tSymbol: " + Sym);
            Console.WriteLine(tab + "}");
        }

        public override Type.Type CheckType(Env env)
        {
            return new Type.ArrayType((Type.Type)env.typeEnv.Get(Symbol.Symbol.Intern(this.Sym)));
        }
    }

    public class RecordTypeNode : ASTTypeNode
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

        public override Type.Type CheckType(Env env)
        {
            List<Type.RecordField> fields = new List<Type.RecordField>();
            foreach (Field f in Fields)
            {
                Type.RecordField rf = new Type.RecordField(Symbol.Symbol.Intern(f.NameSymbol), (Type.Type)env.typeEnv.Get(Symbol.Symbol.Intern(f.TypeSymbol)));
                fields.Add(rf);
            }

            return new Type.RecordType(fields);
        }
    }

}
