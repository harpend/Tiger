using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.ANTLR.AST.Node;

namespace Tiger.ANTLR.AST
{
    class TigerVisitor : TigerBaseVisitor<ASTNode>
    {
        public override ASTNode VisitIntegerLiteral([NotNull] TigerParser.IntegerLiteralContext context)
        {
            return new IntExprNode(int.Parse(context.INTLIT().GetText()));
        }

        public override ASTNode VisitStringLiteral([NotNull] TigerParser.StringLiteralContext context)
        {
            return new StringExprNode(context.STRLIT().GetText());
        }

        public override ASTNode VisitNil([NotNull] TigerParser.NilContext context)
        {
            return new NilExprNode();
        }

        public override ASTNode VisitFunctionCall([NotNull] TigerParser.FunctionCallContext context)
        {
            string functionName = context.ID().GetText();
            List<ASTExprNode> args = context.expr()
                .Select(exprCtx => Visit(exprCtx) as ASTExprNode)
                .ToList();

            int position = context.Start.StartIndex;
            return new CallExprNode(args, position, functionName);
        }

        public override ASTNode VisitRecordCreation([NotNull] TigerParser.RecordCreationContext context)
        {
            string typeName = context.typeid().GetText();
            List<RecordExprNode.RecordField> fields = new List<RecordExprNode.RecordField>();
            for (int i = 0; i < context.ID().Length; i++)
            {
                string fieldName = context.ID()[i].GetText();
                ASTExprNode expr = Visit(context.expr()[i]) as ASTExprNode;
                int pos = context.ID()[i].Symbol.StartIndex;
                fields.Add(new RecordExprNode.RecordField(fieldName, expr, pos));
            }

            int position = context.Start.StartIndex;
            return new RecordExprNode(fields, typeName, position);
        }

        public override ASTNode VisitArrayCreation([NotNull] TigerParser.ArrayCreationContext context)
        {
            string symbolName = context.typeid().GetText();
            ASTExprNode size = Visit(context.expr()[0]) as ASTExprNode;
            ASTExprNode init = Visit(context.expr()[1]) as ASTExprNode;
            int position = context.Start.StartIndex;

            return new ArrayExprNode(symbolName, size, init, position);
        }

        public override ASTNode VisitNegationExpr([NotNull] TigerParser.NegationExprContext context)
        {
            // For this you have to represent it as 0 - expr
            ASTExprNode left = new IntExprNode(0);
            ASTExprNode right = Visit(context.expr()) as ASTExprNode;
            ASTOpNode op = new MinusOpNode();
            return new OpExprNode(left, right, op);
        }

        public override ASTNode VisitMultExpr([NotNull] TigerParser.MultExprContext context)
        {
            ASTExprNode left = Visit(context.expr()[0]) as ASTExprNode;
            ASTExprNode right = Visit(context.expr()[1]) as ASTExprNode;
            ASTOpNode op = new TimesOpNode();
            return new OpExprNode(left, right, op);
        }

        public override ASTNode VisitDivExpr(TigerParser.DivExprContext context)
        {
            ASTExprNode left = Visit(context.expr()[0]) as ASTExprNode;
            ASTExprNode right = Visit(context.expr()[1]) as ASTExprNode;
            ASTOpNode op = new DivideOpNode();
            return new OpExprNode(left, right, op);
        }

        public override ASTNode VisitAddExpr([NotNull] TigerParser.AddExprContext context)
        {
            ASTExprNode left = Visit(context.expr()[0]) as ASTExprNode;
            ASTExprNode right = Visit(context.expr()[1]) as ASTExprNode;
            ASTOpNode op = new PlusOpNode();
            return new OpExprNode(left, right, op);
        }

        public override ASTNode VisitSubExpr([NotNull] TigerParser.SubExprContext context)
        {
            ASTExprNode left = Visit(context.expr()[0]) as ASTExprNode;
            ASTExprNode right = Visit(context.expr()[1]) as ASTExprNode;
            ASTOpNode op = new MinusOpNode();
            return new OpExprNode(left, right, op);
        }

        public override ASTNode VisitNeqExpr([NotNull] TigerParser.NeqExprContext context)
        {
            ASTExprNode left = Visit(context.expr()[0]) as ASTExprNode;
            ASTExprNode right = Visit(context.expr()[1]) as ASTExprNode;
            ASTOpNode op = new NeqOpNode();
            return new OpExprNode(left, right, op);
        }

        public override ASTNode VisitLtExpr([NotNull] TigerParser.LtExprContext context)
        {
            ASTExprNode left = Visit(context.expr()[0]) as ASTExprNode;
            ASTExprNode right = Visit(context.expr()[1]) as ASTExprNode;
            ASTOpNode op = new LtOpNode();
            return new OpExprNode(left, right, op);
        }

        public override ASTNode VisitGtExpr([NotNull] TigerParser.GtExprContext context)
        {
            ASTExprNode left = Visit(context.expr()[0]) as ASTExprNode;
            ASTExprNode right = Visit(context.expr()[1]) as ASTExprNode;
            ASTOpNode op = new GtOpNode();
            return new OpExprNode(left, right, op);
        }

        public override ASTNode VisitLeExpr([NotNull] TigerParser.LeExprContext context)
        {
            ASTExprNode left = Visit(context.expr()[0]) as ASTExprNode;
            ASTExprNode right = Visit(context.expr()[1]) as ASTExprNode;
            ASTOpNode op = new LeOpNode();
            return new OpExprNode(left, right, op);
        }

        public override ASTNode VisitGeExpr([NotNull] TigerParser.GeExprContext context)
        {
            ASTExprNode left = Visit(context.expr()[0]) as ASTExprNode;
            ASTExprNode right = Visit(context.expr()[1]) as ASTExprNode;
            ASTOpNode op = new GeOpNode();
            return new OpExprNode(left, right, op);
        }

        public override ASTNode VisitEqExpr([NotNull] TigerParser.EqExprContext context)
        {
            ASTExprNode left = Visit(context.expr()[0]) as ASTExprNode;
            ASTExprNode right = Visit(context.expr()[1]) as ASTExprNode;
            ASTOpNode op = new EqOpNode();
            return new OpExprNode(left, right, op);
        }

        public override ASTNode VisitAndExpr([NotNull] TigerParser.AndExprContext context)
        {
            ASTExprNode then_ = Visit(context.expr()[1]) as ASTExprNode;
            ASTExprNode else_ = new IntExprNode(0);
            ASTExprNode test = Visit(context.expr()[0]) as ASTExprNode;
            return new IfExprNode(test, then_, else_);
        }

        public override ASTNode VisitOrExpr([NotNull] TigerParser.OrExprContext context)
        {
            ASTExprNode else_ = Visit(context.expr()[1]) as ASTExprNode;
            ASTExprNode then_ = new IntExprNode(1);
            ASTExprNode test = Visit(context.expr()[0]) as ASTExprNode;
            return new IfExprNode(test, then_, else_);
        }

        public override ASTNode VisitParenNestExpr([NotNull] TigerParser.ParenNestExprContext context)
        {
            return Visit(context.expr()) as ASTExprNode;
        }

        public override ASTNode VisitAssignment([NotNull] TigerParser.AssignmentContext context)
        {
            ASTVarNode var = Visit(context.lvalue()) as ASTVarNode;
            ASTExprNode expr = Visit(context.expr()) as ASTExprNode;
            int pos = context.Start.StartIndex;
            return new AssignExprNode(expr, pos, var);
        }

        public override ASTNode VisitIfExpr([NotNull] TigerParser.IfExprContext context)
        {
            ASTExprNode test = Visit(context.expr()[0]) as ASTExprNode;
            ASTExprNode then_ = Visit(context.expr()[1]) as ASTExprNode;
            ASTExprNode else_ = null;
            if (context.expr().Length > 2)
            {
                else_ = Visit(context.expr()[2]) as ASTExprNode;
            }
            return new IfExprNode(test, then_, else_);
        }

        public override ASTNode VisitWhileExpr([NotNull] TigerParser.WhileExprContext context)
        {
            return new WhileExprNode();
        }
    }
}
