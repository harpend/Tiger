﻿//------------------------------------------------------------------------------
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
	/// Visit a parse tree produced by <see cref="TigerParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] TigerParser.ProgramContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.line"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLine([NotNull] TigerParser.LineContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatement([NotNull] TigerParser.StatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.ifStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfStmt([NotNull] TigerParser.IfStmtContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.elseIfStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElseIfStmt([NotNull] TigerParser.ElseIfStmtContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlock([NotNull] TigerParser.BlockContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.whileStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhileStmt([NotNull] TigerParser.WhileStmtContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignment([NotNull] TigerParser.AssignmentContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitType([NotNull] TigerParser.TypeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCall([NotNull] TigerParser.FunctionCallContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression([NotNull] TigerParser.ExpressionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.mathOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMathOp([NotNull] TigerParser.MathOpContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.boolOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBoolOp([NotNull] TigerParser.BoolOpContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="TigerParser.constant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstant([NotNull] TigerParser.ConstantContext context);
}
} // namespace Tiger.ANTLR
