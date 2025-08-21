using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Semant;
using Tiger.Table;
using Tiger.Symbol;
using Tiger.Translate;
using Tiger.Type;

namespace Tiger.ANTLR.AST.Node
{
    public abstract class ASTVarNode : ASTNode
    {
        public int Pos { get; }

        protected ASTVarNode(int pos)
        {
            this.Pos = pos;
        }

        public abstract Type.Type CheckType(Env env);
        public abstract ExprTy TransVar(Env env);
    }

    public class SimpleVarNode : ASTVarNode
    {
        public string Sym { get; }
        public SimpleVarNode(string symbol, int pos) : base(pos)
        {
            this.Sym = symbol;
        }
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "SimpleVar{");
            Console.WriteLine(tab + "\tSymbol: " + Sym);
            Console.WriteLine(tab + "}");
        }

        public override Type.Type CheckType(Env env) // not one type for this, therefore it should never be checked or maybe return the first type.
        {
            VarEntry ve = (VarEntry)env.varEnv.Get(Symbol.Symbol.Intern(Sym));
            if (ve != null)
            {
                return ve.type;
            }

            throw new Exception(Error.Error.NonExistantType);
        }

        public override ExprTy TransVar(Env env)
        {
            Type.Type type = CheckType(env);
            return new ExprTy(type, new DummyExpr());
        }
    }

    class FieldVarNode : ASTVarNode
    {
        public ASTVarNode Var { get; }
        public string Sym { get; }

        public FieldVarNode(ASTVarNode var, string symbol, int pos) : base(pos)
        {
            this.Sym = symbol;
            this.Var = var;
        }

        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "FieldVar{");
            Console.WriteLine(tab + "\tSymbol: ", Sym);
            Console.WriteLine(tab + "\tVar: ");
            this.Var.printNode(tab + "\t\t");
            Console.WriteLine(tab + "}");
        }

        public override Type.Type CheckType(Env env) // not one type for this, therefore it should never be checked or maybe return the first type.
        {
            RecordType type = (RecordType)Var.CheckType(env);
            if (type != null)
            {
                foreach (RecordField rf in type.recordFields)
                {
                    if (rf.name == Symbol.Symbol.Intern(Sym))
                    {
                        return rf.type;
                    }
                }
            }

            throw new Exception(Error.Error.NonExistantType); 
        }

        public override ExprTy TransVar(Env env)
        {
            Type.Type type = CheckType(env);
            return new ExprTy(type, new DummyExpr());
        }
    }

    class SubscriptVarNode : ASTVarNode
    {
        public ASTVarNode Var { get; }
        public ASTExprNode Expr { get; }

        public SubscriptVarNode(ASTVarNode var, ASTExprNode expr, int pos) : base(pos)
        {
            this.Expr = expr;
            this.Var = var;
        }
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "SubscriptVar{");
            Console.WriteLine(tab + "\tExpr: ");
            this.Expr.printNode(tab + "\t\t");
            Console.WriteLine(tab + "\tVar: ");
            this.Var.printNode(tab + "\t\t");
            Console.WriteLine(tab + "}");
        }
        public override Type.Type CheckType(Env env) // not one type for this, therefore it should never be checked or maybe return the first type.
        {
            ArrayType type = (ArrayType)Var.CheckType(env);
            Type.Type type2 = Expr.CheckType(env);
            
            if (type != null && type2 != null && type2.Equals(env.typeEnv.Get(Symbol.Symbol.Intern("int"))))
            {
                return type.type;
            }

            throw new Exception(Error.Error.NonExistantType);
        }

        public override ExprTy TransVar(Env env)
        {
            Type.Type type = CheckType(env);
            return new ExprTy(type, new DummyExpr());
        }
    }
}
