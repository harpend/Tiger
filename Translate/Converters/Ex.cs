using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Frame;
using Tiger.IR;

namespace Tiger.Translate.Converters
{
    public class Ex : IExpr
    {
        public IR.Expr e;
        public Ex(IR.Expr e) {
            this.e = e;
        }

        public IR.Expr UnEx()
        {
            return e;
        }

        public IR.Stmt UnNx()
        {
            return new Expr(e);
        }

        public IR.Stmt UnCx(Frame.Label l1, Frame.Label l2)
        {
            return new IR.CJump(IR.RelOp.NE, e, new IR.Const(0), l1, l2);
        }
    }
}
