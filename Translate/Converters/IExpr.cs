using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Frame;

namespace Tiger.Translate.Converters
{
    public interface IExpr
    {
        public IR.Expr UnEx();
        public IR.Stmt UnNx();
        public IR.Stmt UnCx(Label l1, Label l2);
    }
}
