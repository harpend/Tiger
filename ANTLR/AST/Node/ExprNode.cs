using Antlr4.Runtime.Tree;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tiger.Table;

namespace Tiger.ANTLR.AST.Node
{
    abstract class ASTExprNode : ASTNode {
        public abstract TigerType CheckType(SymbolTable symbolTable, TypeTable typeTable);
    }

    class ExprsNode : ASTExprNode
    {
        public List<ASTExprNode> Exprs { get; }
        public ExprsNode(List<ASTExprNode> exprs)
        {
            this.Exprs = exprs;
        }
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "Exprs {");
            Console.WriteLine(tab + "\tExprs[ ");
            foreach (ASTExprNode e in this.Exprs)
            {
                e.printNode(tab + "\t\t");
            }
            Console.WriteLine("]");
            Console.WriteLine(tab + "}");
        }

        public override TigerType CheckType(SymbolTable symbolTable, TypeTable typeTable) // not one type for this, therefore it should never be checked or maybe return the first type.
        {
            throw new Exception(Error.TypeError.NonExistantType());
        }
    }
    class VarExprNode : ASTExprNode
    {
        public ASTVarNode Var { get; }
        public VarExprNode(ASTVarNode var)
        {
            this.Var = var;
        }
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "VarExpr {");
            Console.WriteLine(tab + "\tvar: ");
            this.Var.printNode(tab + "\t");
            Console.WriteLine(tab + "}");
        }

        public override TigerType CheckType(SymbolTable symbolTable, TypeTable typeTable)
        {
            return this.Var.CheckType(symbolTable, typeTable);
        }
    }

    class NilExprNode : ASTExprNode {
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "Nil {");
            Console.WriteLine(tab + "}");
        }

        public override TigerType CheckType(SymbolTable symbolTable, TypeTable typeTable)
        {
            return typeTable.Get("nil");
        }
    }

    class IntExprNode : ASTExprNode
    {
        public int Value { get; }
        public IntExprNode(int value)
        {
            this.Value = value;
        }
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "Int {");
            Console.WriteLine(tab + "\tint: " + this.Value);
            Console.WriteLine(tab + "}");
        }
        public override TigerType CheckType(SymbolTable symbolTable, TypeTable typeTable)
        {
            return typeTable.Get("int");
        }
    }

    class StringExprNode : ASTExprNode
    {
        public string Value { get; }
        public StringExprNode(string value)
        {
            this.Value = value;
        }
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "String {");
            Console.WriteLine(tab + "\tstring: ", this.Value);
            Console.WriteLine(tab + "}");
        }
        public override TigerType CheckType(SymbolTable symbolTable, TypeTable typeTable)
        {
            return typeTable.Get("string");
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
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "Call {");
            Console.WriteLine(tab + "\tName: " + this.FuncSymbol);
            Console.WriteLine(tab + "\tArgs[ ");
            foreach (ASTExprNode e in this.Args)
            {
                e.printNode(tab + "\t\t");
            }
            Console.WriteLine(tab + "\t]");
            Console.WriteLine(tab + "}");
        }

        public override TigerType CheckType(SymbolTable symbolTable, TypeTable typeTable)
        {
            Symbol s = symbolTable.Get(this.FuncSymbol);
            FuncSym fn = s.entry as FuncSym;
            if (fn == null || !s.IsFunction()) throw new Exception(Error.TypeError.NonExistantType());
            return fn.retType;
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

        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "Op {");
            Console.WriteLine(tab + "\tLeft: ");
            this.Left.printNode(tab + "\t\t");
            Console.WriteLine(tab + "\tOp: ");
            this.Op.printNode(tab + "\t\t");
            Console.WriteLine(tab + "\tRight: ");
            this.Right.printNode(tab + "\t\t");
            Console.WriteLine(tab + "}");
        }

        public override TigerType CheckType(SymbolTable symbolTable, TypeTable typeTable)
        {
            TigerType leftType = this.Left.CheckType(symbolTable, typeTable);
            TigerType rightType = this.Right.CheckType(symbolTable, typeTable);
            if (leftType.typeName.Equals("string") && rightType.typeName.Equals("string")) {
                return leftType;
            } else if (leftType.Equals("int") && rightType.typeName.Equals("int")) {
                return leftType;
            }

            throw new Exception(Error.TypeError.InvalidOperation(leftType.typeName, rightType.typeName));
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

        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "Record {");
            Console.WriteLine(tab + "\tTypeSymbol: " + this.TypeSymbol);

            foreach (var field in Fields)
            {
                Console.WriteLine(tab + "\tRecordField {");
                Console.WriteLine(tab + "\t\tSymbol: " + field.Symbol);
                Console.WriteLine(tab + "\t\tExpr: ");
                field.Expr.printNode(tab + "\t\t\t");
                Console.WriteLine(tab + "\t}");
            }

            Console.WriteLine(tab + "}");
        }

        public override TigerType CheckType(SymbolTable symbolTable, TypeTable typeTable)
        {
            TigerType tt = typeTable.Get(this.TypeSymbol);
            if (tt == null) throw new Exception(Error.TypeError.NonExistantType());
            return tt;
        }
    }

    class SeqExprNode : ASTExprNode // currently not used anywhere
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
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "Seq {");

            foreach (var sequence in Sequences)
            {
                Console.WriteLine(tab + "\tSequence {");
                Console.WriteLine(tab + "\t\tExpr: ");
                sequence.Expr.printNode(tab + "\t\t\t");
                Console.WriteLine(tab + "\t}");
            }

            Console.WriteLine(tab + "}");
        }

        public override TigerType CheckType(SymbolTable symbolTable, TypeTable typeTable) // not one type for this, therefore it should never be checked or maybe return the first type.
        {
            throw new Exception(Error.TypeError.NonExistantType());
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

        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "AssignExpr {");
            Console.WriteLine(tab + "\tVar: ");
            Var.printNode(tab + "\t\t");
            Console.WriteLine(tab + "\tExpr: ");
            Expr.printNode(tab + "\t\t");
            Console.WriteLine(tab + "}");
        }

        public override TigerType CheckType(SymbolTable symbolTable, TypeTable typeTable)
        {
            throw new Exception(Error.TypeError.NonExistantType());
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

        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "IfExpr {");
            Console.WriteLine(tab + "\tTest: ");
            Test.printNode(tab + "\t\t");
            Console.WriteLine(tab + "\tThen: ");
            Then_.printNode(tab + "\t\t");
            if (Else_ != null)
            {
                Console.WriteLine(tab + "\tElse: ");
                Else_.printNode(tab + "\t\t");
            }
            else
            {
                Console.WriteLine(tab + "\tElse: null");
            }

            Console.WriteLine(tab + "}");
        }

        public override TigerType CheckType(SymbolTable symbolTable, TypeTable typeTable)
        {
            TigerType t1 = this.Test.CheckType(symbolTable, typeTable);
            TigerType t2 = this.Then_.CheckType(symbolTable, typeTable);
            TigerType t3 = this.Else_.CheckType(symbolTable, typeTable);
            if (t1 == t2 && t2 == t3)
            {
                return t1;
            }

            throw new Exception(Error.TypeError.NonExistantType());
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

        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "WhileExpr {");
            Console.WriteLine(tab + "\tTest: ");
            Test.printNode(tab + "\t\t");

            Console.WriteLine(tab + "\tBody: ");
            Body.printNode(tab + "\t\t");

            Console.WriteLine(tab + "}");
        }

        public override TigerType CheckType(SymbolTable symbolTable, TypeTable typeTable)
        {
            throw new Exception(Error.TypeError.NonExistantType());
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

        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "ForExpr {");
            Console.WriteLine(tab + "\tVar: ");
            Var.printNode(tab + "\t\t");
            Console.WriteLine(tab + "\tLo: ");
            Lo.printNode(tab + "\t\t");
            Console.WriteLine(tab + "\tHi: ");
            Hi.printNode(tab + "\t\t");
            Console.WriteLine(tab + "\tBody: ");
            Body.printNode(tab + "\t\t");
            Console.WriteLine(tab + "}");
        }

        public override TigerType CheckType(SymbolTable symbolTable, TypeTable typeTable)
        {
            throw new Exception(Error.TypeError.NonExistantType());
        }
    }

    class BreakExprNode : ASTExprNode
    {
        public int Pos { get; }
        public BreakExprNode(int pos) { this.Pos = pos; }

        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "Break {");
            Console.WriteLine(tab + "}");
        }

        public override TigerType CheckType(SymbolTable symbolTable, TypeTable typeTable)
        {
            throw new Exception(Error.TypeError.NonExistantType());
        }
    }

    class LetExprNode : ASTExprNode
    {
        public DecsNode Decs { get; }
        public ExprsNode Body { get; }
        public int Pos { get; }
        public LetExprNode(DecsNode decs, ExprsNode body, int pos)
        {
            this.Decs = decs;
            this.Body = body;
            this.Pos = pos;
        }

        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "LetExpr {");
            Console.WriteLine(tab + "\tDeclarations: ");
            Decs.printNode(tab + "\t\t");
            Console.WriteLine(tab + "\tBody: ");
            Body.printNode(tab + "\t\t");
            Console.WriteLine(tab + "}");
        }

        public override TigerType CheckType(SymbolTable symbolTable, TypeTable typeTable) 
        {
            throw new Exception(Error.TypeError.NonExistantType());
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

        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "ArrayExpr {");
            Console.WriteLine(tab + "\tSymbol: " + this.Symbol);
            Console.WriteLine(tab + "\tSize: ");
            this.Size.printNode(tab + "\t\t");
            Console.WriteLine(tab + "\tInit: ");
            this.Init.printNode(tab + "\t\t");
            Console.WriteLine(tab + "}");
        }

        public override TigerType CheckType(SymbolTable symbolTable, TypeTable typeTable)
        {
            TigerType tt = typeTable.Get(this.Symbol);
            if (tt == null) throw new Exception(Error.TypeError.NonExistantType());
            return tt;
        }
    }
    
}