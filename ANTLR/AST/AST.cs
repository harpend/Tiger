using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            ASTExprNode test = Visit(context.expr()[0]) as ASTExprNode;
            ASTExprNode body = Visit(context.expr()[1]) as ASTExprNode;
            int pos = context.Start.StopIndex;
            return new WhileExprNode(test, pos, body);
        }

        public override ASTNode VisitForExpr([NotNull] TigerParser.ForExprContext context)
        {
            string varName = context.ID().GetText();
            int pos = context.ID().Symbol.StartIndex; // Assuming position is based on line number
            VarExprNode loopVar = new VarExprNode(new SimpleVarNode(varName, pos));
            ASTExprNode lo = Visit(context.expr()[0]) as ASTExprNode ;
            ASTExprNode hi = Visit(context.expr()[1]) as ASTExprNode ;
            ASTExprNode body = Visit(context.expr()[2]) as ASTExprNode;
            pos = context.Start.StartIndex;
            bool escape = true;
            return new ForExprNode(loopVar, escape, lo, hi, body, pos);
        }

        public override ASTNode VisitBreakExpr([NotNull] TigerParser.BreakExprContext context)
        {
            return new BreakExprNode(context.Start.StartIndex);
        }

        public override ASTNode VisitLetExpr([NotNull] TigerParser.LetExprContext context)
        {
            DecsNode decs = Visit(context.decs()) as DecsNode; 
            ExprsNode exprs = Visit(context.exprs()) as ExprsNode;
            int pos = context.Start.StartIndex;
            return new LetExprNode(decs, exprs, pos);
        }

        public override ASTNode VisitExprs([NotNull] TigerParser.ExprsContext context)
        {
            List<ASTExprNode> exprs = context.expr()
                .Select(exprCtx => Visit(exprCtx) as ASTExprNode)
                .ToList();
            return new ExprsNode(exprs);
        }
        public override ASTNode VisitDecs([NotNull] TigerParser.DecsContext context)
        {
            List<ASTDecNode> decs = context.dec()
                .Select(exprCtx => Visit(exprCtx) as ASTDecNode)
                .ToList();
            return new DecsNode(decs);
        }

        public override ASTNode VisitTyTypeId([NotNull] TigerParser.TyTypeIdContext context)
        {
            string symbol = context.typeid().ID().GetText();
            int pos = context.Start.StartIndex;
            return new NameTypeNode(symbol, pos);
        }

        public override ASTNode VisitTyArray([NotNull] TigerParser.TyArrayContext context)
        {
            string symbol = context.typeid().ID().GetText();
            int pos = context.Start.StartIndex;
            return new ArrayTypeNode(symbol, pos);
        }

        public override ASTNode VisitTyBraced([NotNull] TigerParser.TyBracedContext context)
        {
            return Visit(context.tyfields());
        }

        public override ASTNode VisitTyfields([NotNull] TigerParser.TyfieldsContext context)
        {
            List<Field> fields = context.field()
                .Select(fieldCtx => Visit(fieldCtx) as Field)
                .ToList();
            return new RecordTypeNode(fields);
        }

        public override ASTNode VisitField([NotNull] TigerParser.FieldContext context)
        {
            string name = context.ID().GetText();
            int pos = context.start.StartIndex;
            string type = context.typeid().GetText();
            return new Field(name, true, type, pos);
        }

        //public override ASTNode VisitTydec([NotNull] TigerParser.TydecContext context)
        //{
        //    List<TypeDecNode> typeDecNodes = new List<TypeDecNode>();

        //    // Loop through the children of the context to process the type declarations
        //    do
        //    {
        //        string name = context.ID().GetText();  
        //        string type = context.typeid().GetText();  

        //        // Create a TypeDecNode and add it to the list
        //        typeDecNodes.Add(new TypeDecNode(name, type));

        //        context = context.parent as TigerParser.TydecContext;
        //        // https://stackoverflow.com/questions/28457534/antlr4-get-left-and-right-sibling-of-rule-context

        //    } while (context != null && context is TigerParser.TydecContext);
        //}
    }
}
