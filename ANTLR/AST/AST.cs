using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.ANTLR.AST
{
    public class TigerVisitor : TigerBaseVisitor<string>
    {
        public override string VisitNil([NotNull] TigerParser.NilContext context)
        {
            return base.VisitNil(context);
        }
    }
}
