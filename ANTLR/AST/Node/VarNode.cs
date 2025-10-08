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
using Tiger.Tree;
using Tiger.Frame;

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
        public abstract ExprTy TransVar(Env env, Stack<Level> levelStack);
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

        public override ExprTy TransVar(Env env, Stack<Level> levelStack)
        {
            Type.Type type = CheckType(env);
            VarEntry ve = (VarEntry)env.varEnv.Get(Symbol.Symbol.Intern(Sym));
            Level l = levelStack.Peek();
            Expr expr = new Temp(l.Frame.GetFP());
            while (!l.Equals(ve.access.level))
            {
                List<Access> accesses = Translate.Translate.Formals(l);
                Access link = accesses.First();
                InFrame inf = (InFrame)link.fAccess;
                expr = new Mem(new BinOpExpr(BinOp.PLUS, expr, new Const(inf.Offset)));
                l = link.level;
            }

            // after adding the offset to all the static links, add the offset to the variable
            InFrame inf2 = (InFrame)ve.access.fAccess;
            expr = new Mem(new BinOpExpr(BinOp.PLUS, expr, new Const(inf2.Offset)));
            return new ExprTy(type, new Ex(expr));
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

        public override ExprTy TransVar(Env env, Stack<Level> levelStack)
        {
            Type.Type type = CheckType(env);
            ExprTy exprTy = Var.TransVar(env, levelStack);
            RecordType rtype = (RecordType)type;
            int i = 0;
            for (; i < rtype.recordFields.Count; i++) {
                RecordField rf = rtype.recordFields[i];
                if (rf.name == Symbol.Symbol.Intern(Sym))
                {
                    break;
                }
            }
            
            Expr offset = new BinOpExpr(BinOp.MUL, new Const(i), new Const(8)); // this wouldn't work if variables were not constant size
            Expr address = new BinOpExpr(BinOp.PLUS, exprTy.expr.UnEx(), offset);
            Expr expr = new Mem(address);
            return new ExprTy(type, new Ex(expr));
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

        public override ExprTy TransVar(Env env, Stack<Level> levelStack)
        {
            Type.Type type = CheckType(env);
            ExprTy exprTy = Var.TransVar(env, levelStack);
            ExprTy indexExprTy = Expr.TransExpr(env, levelStack);
            Expr offset = new BinOpExpr(BinOp.MUL, indexExprTy.expr.UnEx(), new Const(8));
            Expr address = new BinOpExpr(BinOp.PLUS, exprTy.expr.UnEx(), offset);
            Expr expr = new Mem(address);
            return new ExprTy(type, new Ex(expr));
        }
    }
}
