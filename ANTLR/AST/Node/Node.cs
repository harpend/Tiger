using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Semant;
using Tiger.Translate;

namespace Tiger.ANTLR.AST.Node
{
    public abstract class ASTNode {
        public abstract void printNode(string tab);
    }
    public class Field : ASTNode
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
    public class FuncDec : ASTNode
    {
        public string NameSymbol { get; }
        public List<Field> Fields { get; }
        public NameTypeNode Option { get; }
        public ASTExprNode Body { get; }
        public int Pos { get; }
        public FuncDec(string nameSymbol, List<Field> fields, NameTypeNode option, ASTExprNode body, int pos)
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

        public ExprTy TransDec(Env env)
        {
            List<Type.Type> types = new List<Type.Type>();  
            foreach (Field f in this.Fields)
            {
                Type.Type tt = (Type.Type)env.typeEnv.Get(Symbol.Symbol.Intern(f.TypeSymbol));
                if (tt == null) throw new Exception(Error.Error.NonExistantType);
                types.Add(tt);
            }

            Type.Type result = null;    
            if (Option == null)
            {
                env.varEnv.Push(Symbol.Symbol.Intern(this.NameSymbol), new FunEntry(types, null));
            } else
            {
                result = Option.CheckType(env);
                env.varEnv.Push(Symbol.Symbol.Intern(this.NameSymbol), new FunEntry(types, result));
            }

            env.varEnv.BeginScope();
            env.typeEnv.BeginScope();
            foreach (Field f in this.Fields)
            {
                Type.Type tt = (Type.Type)env.typeEnv.Get(Symbol.Symbol.Intern(f.TypeSymbol));
                if (tt == null) throw new Exception(Error.Error.NonExistantType);
                env.varEnv.Push(Symbol.Symbol.Intern(f.NameSymbol), new VarEntry(tt));
            }

            // TODO: evaluate body
            env.typeEnv.EndScope();
            env.varEnv.EndScope();
            return new ExprTy(result, new DummyExpr());
        }
    }

    public class ProgramNode : ASTNode
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

        public List<ExprTy> TransProg(Env env)
        {
            foreach (DecsNode dn in this.Decs)
            {
                dn.TransDec(env);
            }

            List<ExprTy> exprTyList = new List<ExprTy>();
            foreach (ASTExprNode e in this.Expressions)
            {
                exprTyList.Add(e.TransExpr(env));
            }

            return exprTyList;
        }
    }
}
