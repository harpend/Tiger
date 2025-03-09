//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\jackt\OneDrive\Documents\Compilers\Tiger\Tiger\ANTLR\Tiger.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Tiger.ANTLR {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="TigerParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public interface ITigerVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by the <c>DecFunDec</c>
	/// labeled alternative in <see cref="TigerParser.dec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDecFunDec([NotNull] TigerParser.DecFunDecContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>DecVarDec</c>
	/// labeled alternative in <see cref="TigerParser.dec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDecVarDec([NotNull] TigerParser.DecVarDecContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>DecImport</c>
	/// labeled alternative in <see cref="TigerParser.dec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDecImport([NotNull] TigerParser.DecImportContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>DecTyDec</c>
	/// labeled alternative in <see cref="TigerParser.dec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDecTyDec([NotNull] TigerParser.DecTyDecContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>TyTypeId</c>
	/// labeled alternative in <see cref="TigerParser.ty"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTyTypeId([NotNull] TigerParser.TyTypeIdContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>TyArray</c>
	/// labeled alternative in <see cref="TigerParser.ty"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTyArray([NotNull] TigerParser.TyArrayContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>TyBraced</c>
	/// labeled alternative in <see cref="TigerParser.ty"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTyBraced([NotNull] TigerParser.TyBracedContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>SimpleFuncDec</c>
	/// labeled alternative in <see cref="TigerParser.fundec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSimpleFuncDec([NotNull] TigerParser.SimpleFuncDecContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>TypeFuncDec</c>
	/// labeled alternative in <see cref="TigerParser.fundec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeFuncDec([NotNull] TigerParser.TypeFuncDecContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>LvalNest</c>
	/// labeled alternative in <see cref="TigerParser.lvalue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLvalNest([NotNull] TigerParser.LvalNestContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>LvalAttr</c>
	/// labeled alternative in <see cref="TigerParser.lvalue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLvalAttr([NotNull] TigerParser.LvalAttrContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>LvalID</c>
	/// labeled alternative in <see cref="TigerParser.lvalue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLvalID([NotNull] TigerParser.LvalIDContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>SimpleVarDec</c>
	/// labeled alternative in <see cref="TigerParser.vardec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSimpleVarDec([NotNull] TigerParser.SimpleVarDecContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>TypeVarDec</c>
	/// labeled alternative in <see cref="TigerParser.vardec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeVarDec([NotNull] TigerParser.TypeVarDecContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>AndExpr</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAndExpr([NotNull] TigerParser.AndExprContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>WhileExpr</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhileExpr([NotNull] TigerParser.WhileExprContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>IfExpr</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfExpr([NotNull] TigerParser.IfExprContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>LtExpr</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLtExpr([NotNull] TigerParser.LtExprContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>GtExpr</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGtExpr([NotNull] TigerParser.GtExprContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>GeExpr</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGeExpr([NotNull] TigerParser.GeExprContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>LeExpr</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLeExpr([NotNull] TigerParser.LeExprContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>Assignment</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignment([NotNull] TigerParser.AssignmentContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>RecordCreation</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRecordCreation([NotNull] TigerParser.RecordCreationContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>NeqExpr</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNeqExpr([NotNull] TigerParser.NeqExprContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>FunctionCall</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCall([NotNull] TigerParser.FunctionCallContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>ParenNestExpr</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParenNestExpr([NotNull] TigerParser.ParenNestExprContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>IntegerLiteral</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIntegerLiteral([NotNull] TigerParser.IntegerLiteralContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>LetExpr</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLetExpr([NotNull] TigerParser.LetExprContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>MultExpr</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMultExpr([NotNull] TigerParser.MultExprContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>SubExpr</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubExpr([NotNull] TigerParser.SubExprContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>AddExpr</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAddExpr([NotNull] TigerParser.AddExprContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>OrExpr</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOrExpr([NotNull] TigerParser.OrExprContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>Nil</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNil([NotNull] TigerParser.NilContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>DivExpr</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDivExpr([NotNull] TigerParser.DivExprContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>StringLiteral</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStringLiteral([NotNull] TigerParser.StringLiteralContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>EqExpr</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEqExpr([NotNull] TigerParser.EqExprContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>ArrayCreation</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArrayCreation([NotNull] TigerParser.ArrayCreationContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>ForExpr</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForExpr([NotNull] TigerParser.ForExprContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>LeftVal</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLeftVal([NotNull] TigerParser.LeftValContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>NegationExpr</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNegationExpr([NotNull] TigerParser.NegationExprContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>BreakExpr</c>
	/// labeled alternative in <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBreakExpr([NotNull] TigerParser.BreakExprContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] TigerParser.ProgramContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.decs"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDecs([NotNull] TigerParser.DecsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.dec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDec([NotNull] TigerParser.DecContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.tydec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTydec([NotNull] TigerParser.TydecContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.ty"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTy([NotNull] TigerParser.TyContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.tyfields"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTyfields([NotNull] TigerParser.TyfieldsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.field"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitField([NotNull] TigerParser.FieldContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.typeid"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeid([NotNull] TigerParser.TypeidContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.vardec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVardec([NotNull] TigerParser.VardecContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.fundec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFundec([NotNull] TigerParser.FundecContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.lvalue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLvalue([NotNull] TigerParser.LvalueContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.exprs"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExprs([NotNull] TigerParser.ExprsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpr([NotNull] TigerParser.ExprContext context);
}
} // namespace Tiger.ANTLR
