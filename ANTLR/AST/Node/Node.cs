using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

        public ExprTy TransDec(Env env, Stack<Level> levelStack)
        {
            List<Type.Type> types = new List<Type.Type>();
            List<bool> formals = new List<bool>();
            foreach (Field f in this.Fields)
            {
                Type.Type tt = (Type.Type)env.typeEnv.Get(Symbol.Symbol.Intern(f.TypeSymbol));
                if (tt == null) throw new Exception(Error.Error.NonExistantType);
                types.Add(tt);
                formals.Add(true); // currently assumes all params escape
            }

            Frame.Temp.Label label = new Frame.Temp.Label();
            levelStack.Push(Translate.Translate.NewLevel(levelStack.Peek(), label, formals));
            Type.Type result = null;    
            if (Option == null)
            {
                env.varEnv.Push(Symbol.Symbol.Intern(this.NameSymbol), new FunEntry(types, null, label, levelStack.Peek()));
            } else
            {
                result = Option.CheckType(env);
                env.varEnv.Push(Symbol.Symbol.Intern(this.NameSymbol), new FunEntry(types, result, label, levelStack.Peek()));
            }

            env.varEnv.BeginScope();
            env.typeEnv.BeginScope();
            List<Access> fs = Translate.Translate.Formals(levelStack.Peek());
            for (int i = 0; i< this.Fields.Count; i++)
            {
                Field f = this.Fields[i];
                Type.Type tt = (Type.Type)env.typeEnv.Get(Symbol.Symbol.Intern(f.TypeSymbol));
                if (tt == null) throw new Exception(Error.Error.NonExistantType);
                Access a = fs[i + 1]; // +1 due to the static link
                env.varEnv.Push(Symbol.Symbol.Intern(f.NameSymbol), new VarEntry(tt, a));
            }

            // TODO: evaluate body
            env.typeEnv.EndScope();
            env.varEnv.EndScope();
            levelStack.Pop();
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

        public List<ExprTy> TransProg(Env env, Stack<Level> levelStack)
        {
            foreach (DecsNode dn in this.Decs)
            {
                dn.TransDec(env, levelStack);
            }

            List<ExprTy> exprTyList = new List<ExprTy>();
            foreach (ASTExprNode e in this.Expressions)
            {
                exprTyList.Add(e.TransExpr(env, levelStack));
            }

            return exprTyList;
        }
    }
}
