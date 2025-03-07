using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.ANTLR.AST
{
    static class Helpers
    {
        public static TigerParser.TydecContext? GetRightTyDecSibling(this ParserRuleContext context)
        {
            int index = GetNodeIndex(context);
            if (index < 0 || index >= context.Parent.ChildCount) {
                return null;
            }

            IParseTree rightChild = context.Parent.GetChild(index + 1);
            return context.Parent.GetChild(index + 1) as TigerParser.TydecContext;
        }

        public static TigerParser.DecFunDecContext? GetRightFunDecSibling(this ParserRuleContext context)
        {
            int index = GetNodeIndex(context);
            if (index < 0 || index >= context.Parent.ChildCount)
            {
                return null;
            }

            IParseTree rightChild = context.Parent.GetChild(index + 1);
            return context.Parent.GetChild(index + 1) as TigerParser.DecFunDecContext;
        }

        public static int GetNodeIndex(this ParserRuleContext context)
        {
            RuleContext parent = context?.Parent;

            if (parent == null)
                return -1;

            for (int i = 0; i < parent.ChildCount; i++)
            {
                if (parent.GetChild(i) == context)
                    return i;
            }

            return -1;
        }
    }
}
