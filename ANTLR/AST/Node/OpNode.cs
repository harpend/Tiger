using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.ANTLR.AST.Node
{
    abstract class ASTOpNode : ASTNode { }
    class PlusOpNode : ASTOpNode { }
    class MinusOpNode : ASTOpNode { }
    class TimesOpNode : ASTOpNode { }
    class DivideOpNode : ASTOpNode { }
    class EqOpNode : ASTOpNode { }
    class NeqOpNode : ASTOpNode { }
    class LtOpNode : ASTOpNode { }
    class LeOpNode : ASTOpNode { }
    class GtOpNode : ASTOpNode { }
    class GeOpNode : ASTOpNode { }
}
