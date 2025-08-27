using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Tiger.Frame.Temp;
using Tiger.Tree;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tiger.Translate
{
    public interface IExpr
    {
        public Tree.Expr UnEx();
        public Tree.Stmt UnNx();
        public Func<Frame.Temp.Label, Frame.Temp.Label, Tree.Stmt> UnCx();
    }

    public class Ex : IExpr
    {
        public Tree.Expr Expr { get; set; }
        public Ex(Tree.Expr expr)
        {
            Expr = expr;
        }

        public Tree.Expr UnEx()
        {
            return Expr;
        }

        public Tree.Stmt UnNx()
        {
            return Expr;
        }

        public Func<Frame.Temp.Label, Frame.Temp.Label, Tree.Stmt> UnCx()
        {
            return (t, f) =>
                new Tree.CJump(
                    Tree.RelOp.NE,
                    Expr,
                    new Tree.Const(0),
                    t,
                    f
                );
        }

        public class Nx : IExpr
        {
            public Tree.Stmt Stmt { get; set; }
            public Nx(Tree.Stmt stmt)
            {
                Stmt = stmt;
            }

            public Tree.Expr UnEx()
            {
                return new Eseq(Stmt, new Const(0));
            }

            public Tree.Stmt UnNx()
            {
                return Stmt;
            }

            public Func<Frame.Temp.Label, Frame.Temp.Label, Tree.Stmt> UnCx()
            {
                throw new Exception("UnNx should not be called on Nx");
            }
        }

        public class Cx : IExpr
        {
            public Func<Frame.Temp.Label, Frame.Temp.Label, Tree.Stmt> GenStmt { get; }
            public Cx(Func<Frame.Temp.Label, Frame.Temp.Label, Tree.Stmt> genStmt) => GenStmt = genStmt;
            public Tree.Expr UnEx()
            {
                Frame.Temp.Temp r = new Frame.Temp.Temp();
                Frame.Temp.Label t = new Frame.Temp.Label();
                Frame.Temp.Label f = new Frame.Temp.Label();
                return new Eseq(
                    new Seq(new Move(new Tree.Temp(r), new Const(1)),
                            new Seq(GenStmt(t, f), new Seq(new Tree.Label(f),
                            new Seq(new Move(new Tree.Temp(r), new Const(0)), new Tree.Label(t))))),
                            new Tree.Temp(r));
            }

            public Func<Frame.Temp.Label, Frame.Temp.Label, Tree.Stmt> UnCx()
            {
                return GenStmt;
            }

            public Tree.Stmt UnNx()
            {
                return UnEx();
            }
        }

        //public class DummyExpr : IExpr
        //{
        //    public string name { get; set; }
        //    public DummyExpr()
        //    {
        //        this.name = "()";
        //    }
        //}
    }
}
