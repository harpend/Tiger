using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Frame;

namespace Tiger.Translate.Converters
{
    public class Nx : IExpr
    {
        public IR.Stmt stmt;
        public Nx(IR.Stmt stmt)
        {
            this.stmt = stmt;
        }

        public IR.Expr UnEx()
        {
            return new IR.Eseq(this.stmt, new IR.Const(0));
        }

        public IR.Stmt UnNx()
        {
            return this.stmt;
        }

        public IR.Stmt UnCx(Label l1, Label l2)
        {
            Console.WriteLine("Impossible function call");
            Environment.Exit(1);
            return null;
        }
    }
}
