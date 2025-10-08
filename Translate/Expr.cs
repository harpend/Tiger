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
        public Expr UnEx();
        public Stmt UnNx();
        public Func<Frame.Temp.Label, Frame.Temp.Label, Stmt> UnCx();
    }

    public class Ex : IExpr
    {
        public Expr Expr { get; set; }
        public Ex(Expr expr)
        {
            Expr = expr;
        }

        public Expr UnEx()
        {
            return Expr;
        }

        public Stmt UnNx()
        {
            return Expr;
        }

        public Func<Frame.Temp.Label, Frame.Temp.Label, Stmt> UnCx()
        {
            return (t, f) =>
                new CJump(
                    Tree.RelOp.NE,
                    Expr,
                    new Const(0),
                    t,
                    f
                );
        }

        public class Nx : IExpr
        {
            public Stmt Stmt { get; set; }
            public Nx(Stmt stmt)
            {
                Stmt = stmt;
            }

            public Expr UnEx()
            {
                return new Eseq(Stmt, new Const(0));
            }

            public Stmt UnNx()
            {
                return Stmt;
            }

            public Func<Frame.Temp.Label, Frame.Temp.Label, Stmt> UnCx()
            {
                throw new Exception("UnNx should not be called on Nx");
            }
        }

        public class Cx : IExpr
        {
            public Func<Frame.Temp.Label, Frame.Temp.Label, Stmt> GenStmt { get; }
            public Cx(Func<Frame.Temp.Label, Frame.Temp.Label, Stmt> genStmt) => GenStmt = genStmt;
            public Expr UnEx()
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

            public Func<Frame.Temp.Label, Frame.Temp.Label, Stmt> UnCx()
            {
                return GenStmt;
            }

            public Stmt UnNx()
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
