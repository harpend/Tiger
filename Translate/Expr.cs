using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Translate
{
    public interface Expr
    {
    }

    public class DummyExpr : Expr
    {
        public string name { get; set; }
        public DummyExpr()
        {
            this.name = "()";
        }
    }
}
