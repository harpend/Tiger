using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.ANTLR.AST.Node
{
    abstract class ASTExprNode : ASTNode { }

    class VarExprNode : ASTExprNode
    {
        public ASTVarNode Var { get; }
        public VarExprNode(ASTVarNode var)
        {
            this.Var = var;
        }
    }

    class NilExprNode : ASTExprNode { }

    class IntExprNode : ASTExprNode
    {
        public int Value { get; }
        public IntExprNode(int value)
        {
            this.Value = value;
        }
    }

    class StringExprNode : ASTExprNode
    {
        public string Value { get; }
        public StringExprNode(string value)
        {
            this.Value = value;
        }
    }

    class CallExprNode : ASTExprNode
    {
        public string FuncSymbol { get; }
        public List<ASTExprNode> Args { get; }
        public int Pos { get; }

        public CallExprNode(List<ASTExprNode> args, int pos, string funcSymbol)
        {
            this.Args = args;
            this.Pos = pos;
            this.FuncSymbol = funcSymbol;
        }
    }

    class OpExprNode : ASTExprNode
    {
        public ASTExprNode Left { get; }
        public ASTExprNode Right { get; }
        public ASTOpNode Op { get; }

        public OpExprNode(ASTExprNode left, ASTExprNode right, ASTOpNode op)
        {
            this.Left = left;
            this.Right = right;
            this.Op = op;
        }
    }

    class RecordExprNode : ASTExprNode
    {
        public class RecordField
        {
            public string Symbol { get; }
            public ASTExprNode Expr { get; }
            public int Pos { get; }
            public RecordField(string symbol, ASTExprNode expr, int pos)
            {
                this.Symbol = symbol;
                this.Expr = expr;
                this.Pos = pos;
            }
        }

        public List<RecordField> Fields { get; }
        public string TypeSymbol { get; }
        public int Pos { get; }
        public RecordExprNode(List<RecordField> fields, string typeSymbol, int pos)
        {
            this.Fields = fields;
            this.TypeSymbol = typeSymbol;
            this.Pos = pos;
        }
    }

    class SeqExprNode : ASTExprNode
    {
        public class Sequence
        {
            public ASTExprNode Expr { get; }
            public int Pos { get; }
            public Sequence(ASTExprNode expr, int pos)
            {
                this.Expr = expr;
                this.Pos = pos;
            }
        }

        public List<Sequence> Sequences { get; }
        public SeqExprNode(List<Sequence> sequences)
        {
            this.Sequences = sequences;
        }
    }

    class AssignExprNode : ASTExprNode
    {
        public ASTVarNode Var { get; }
        public ASTExprNode Expr { get; }
        public int Pos { get; }

        public AssignExprNode(ASTExprNode assignExpr, int pos, ASTVarNode var)
        {
            this.Var = var;
            this.Expr = assignExpr;
            this.Pos = pos;
        }
    }

    class IfExprNode : ASTExprNode
    {
        public ASTExprNode Test { get; }
        public ASTExprNode Then_ { get; }
        public ASTExprNode Else_ { get; }
        public IfExprNode(ASTExprNode test, ASTExprNode then_, ASTExprNode else_)
        {
            this.Test = test;
            this.Then_ = then_;
            this.Else_ = else_;
        }
    }

    class WhileExprNode : ASTExprNode
    {
        public ASTExprNode Test { get; }
        public ASTExprNode Body { get; }
        public int Pos { get; }
        public WhileExprNode(ASTExprNode test, int pos, ASTExprNode body)
        {
            this.Pos = pos;
            this.Body = body;
            this.Test = test;
        }
    }

    class ForExprNode : ASTExprNode
    {
        public VarExprNode Var { get; }
        public bool BoolRef { get; }
        public ASTExprNode Lo { get; }
        public ASTExprNode Hi { get; }
        public ASTExprNode Body { get; }
        public int Pos { get; }
        public ForExprNode(VarExprNode var, bool boolRef, ASTExprNode lo, ASTExprNode hi, ASTExprNode body, int pos)
        {
            this.Var = var;
            this.BoolRef = boolRef;
            this.Lo = lo;
            this.Hi = hi;
            this.Body = body;
            this.Pos = pos;
        }
    }

    class BreakExprNode : ASTExprNode
    {
        public int Pos { get; }
        public BreakExprNode(int pos) { this.Pos = pos; }
    }

    class LetExprNode : ASTExprNode
    {
        public List<ASTDecNode> Decs { get; }
        public ASTExprNode Body { get; }
        public int Pos { get; }
        public LetExprNode(List<ASTDecNode> decs, ASTExprNode body, int pos)
        {
            this.Decs = decs;
            this.Body = body;
            this.Pos = pos;
        }
    }

    class ArrayExprNode : ASTExprNode
    {
        public string Symbol { get; }
        public ASTExprNode Size { get; }
        public ASTExprNode Init { get; }
        public int Pos { get; }
        public ArrayExprNode(string symbol, ASTExprNode size, ASTExprNode init, int pos)
        {
            this.Symbol = symbol;
            this.Size = size;
            this.Init = init;
            this.Pos = pos;
        }
    }
}