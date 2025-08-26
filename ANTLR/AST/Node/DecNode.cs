using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Semant;
using Tiger.Translate;
using Tiger.Type;

namespace Tiger.ANTLR.AST.Node
{
    public abstract class ASTDecNode : ASTNode {
        public abstract Type.Type CheckType(Env env);
        public abstract ExprTy TransDec(Env env, Stack<Level> levelStack);
    }

    public class DecsNode : ASTDecNode
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

        public override ExprTy TransDec(Env env, Stack<Level> levelStack)
        {
            foreach (var dec in this.Decs)
            {
                dec.TransDec(env, levelStack);
            }

            return null;
        }

        public override Type.Type CheckType(Env env)
        {
            throw new Exception(Error.Error.NonExistantType);
        }
    }
    public class VarDecNode : ASTDecNode
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

        public override ExprTy TransDec(Env env, Stack<Level> levelStack)
        {
            Access a = Translate.Translate.AllocLocal(true, levelStack.Peek());
            Type.Type tt = CheckType(env);
            env.varEnv.Push(Symbol.Symbol.Intern(NameSymbol), new VarEntry(tt, a));
            return new ExprTy(tt, new Translate.DummyExpr());
        }

        public override Type.Type CheckType(Env env)
        {
            Type.Type tt = null;
            Type.Type exprType = this.Init.CheckType(env);
            if (this.Type != null)
            {
                tt = (Type.Type)env.typeEnv.Get(Symbol.Symbol.Intern(this.Type.Symbol));
                if (tt != exprType)
                {
                    throw new Exception(Error.Error.InconsistentType);
                }

                if (!(exprType is not NilType || tt is RecordType)) throw new Exception(Error.Error.NilType);
            }
            else
            {
                if (exprType is NilType) throw new Exception(Error.Error.NilType);
                tt = exprType;
            }
            if (tt == null) throw new Exception(Error.Error.NonExistantType);
            return tt;
        }
    }
        public class FuncDecNode : ASTDecNode
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

            public override Type.Type CheckType(Env env)
            {
                throw new Exception(Error.Error.NonExistantType);
            }
            public override ExprTy TransDec(Env env, Stack<Level> levelStack)
            {
                foreach (var funcDec in FuncDecs)
                {
                    funcDec.TransDec(env, levelStack);
                }

                return new ExprTy(null, new Translate.DummyExpr());
            }
        }

        public class TypeDecNode : ASTDecNode
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
            //public List<TypeSubClass> TypeSubs { get; }
            public TypeSubClass TypeSub { get; }

            public TypeDecNode(TypeSubClass typeSub)
            {
                this.TypeSub = typeSub;
            }

            public override void printNode(string tab)
            {
                Console.WriteLine(tab + "TypeDecNode {");  
                Console.WriteLine(tab + "\tName: " + TypeSub.NameSymbol);
                Console.WriteLine(tab + "\tType: ");
                TypeSub.Ty.printNode(tab + "\t\t");
                Console.WriteLine(tab + "}");
            }

            public override Type.Type CheckType(Env env)
            {
                return TypeSub.Ty.CheckType(env);
            }
            public override ExprTy TransDec(Env env, Stack<Level> levelStack)
            {
                env.typeEnv.Push(Symbol.Symbol.Intern(TypeSub.NameSymbol), CheckType(env));
                return new ExprTy(null, new Translate.DummyExpr());
            }
        }
    }
