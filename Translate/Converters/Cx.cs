using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Tiger.Frame;
using Tiger.IR;

namespace Tiger.Translate.Converters
{
    abstract public class Cx : IExpr
    {

        public IR.Expr UnEx() { // as given in book
            Frame.Temp r = new Frame.Temp();
            Frame.Label l1 = new Frame.Label();
            Frame.Label l2 = new Frame.Label();
            return new Eseq(new Seq(new Move(new IR.Temp(r), new Const(1)),
            new Seq(UnCx(l1, l2),
            new Seq(new IR.Label(l2), new Seq(new Move(new IR.Temp(r), new Const(0)), new IR.Label(l1))))),
                new IR.Temp(r));
        }

        public IR.Stmt UnNx()
        {
            return new IR.Expr(UnEx());
        }

        abstract public IR.Stmt UnCx(Frame.Label l1, Frame.Label l2);
    }
}
