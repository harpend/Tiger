using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Tiger.Semant
{
    public class ExprTy
    {
        public Translate.IExpr expr { get; set; }
        public Type.Type type { get; set; }
        public ExprTy(Type.Type type, Translate.IExpr expr)
        {
            this.type = type;
            this.expr = expr;
        }
    }
}
