using Antlr4.Runtime.Tree;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tiger.Error;
using Tiger.Frame;
using Tiger.Semant;
using Tiger.Table;
using Tiger.Translate;
using Tiger.Tree;
using static System.Net.Mime.MediaTypeNames;
using static Tiger.Translate.Ex;

namespace Tiger.ANTLR.AST.Node
{
    public abstract class ASTExprNode : ASTNode {
        public abstract Type.Type CheckType(Env env);
        public abstract ExprTy TransExpr(Env env, Stack<Level> levelStack);
    }

    public class ExprsNode : ASTExprNode
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

        public override Type.Type CheckType(Semant.Env env) // not one type for this, therefore it should never be checked or maybe return the first type.
        {
            throw new Exception(Error.Error.NonExistantType);
        }

        public override ExprTy TransExpr(Env env, Stack<Level> levelStack)
        {
            Stmt seq;
            List<ExprTy> exprs = new List<ExprTy>();
            foreach (ASTExprNode expr in Exprs)
            {
                ExprTy exprty = expr.TransExpr(env, levelStack);
                exprs.Add(exprty);
            }

            if (exprs.Count == 0)
            {
                return new ExprTy(new Type.NilType(),new Nx(new Const(0)));
            }
            else if (exprs.Count == 1)
            {
                return exprs[0];
            } else
            {
                seq = exprs[0].expr.UnNx();
                // it may be UnEx not UnNx im not sure.
                for (int i = 1; i < exprs.Count; i++) {
                    seq = new Seq(seq, exprs[i].expr.UnNx());
                }
            }

            return new ExprTy(null, new Nx(seq));
        }
    }
    public class VarExprNode : ASTExprNode
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

        public override Type.Type CheckType(Semant.Env env)
        {
            return this.Var.CheckType(env);
        }

        public override ExprTy TransExpr(Env env, Stack<Level> levelStack)
        {
            Type.Type type = CheckType(env);
            return Var.TransVar(env, levelStack);
        }
    }

    public class NilExprNode : ASTExprNode {
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "Nil {");
            Console.WriteLine(tab + "}");
        }

        public override Type.Type CheckType(Semant.Env env)
        {
            return (Type.Type)env.typeEnv.Get(Symbol.Symbol.Intern("nil"));
        }

        public override ExprTy TransExpr(Env env, Stack<Level> levelStack)
        {
            Type.Type type = CheckType(env);
            Const intExpr = new Const(0);
            return new ExprTy(type, new Ex(intExpr));
        }
    }

    public class IntExprNode : ASTExprNode
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
        public override Type.Type CheckType(Semant.Env env)
        {
            return (Type.Type)env.typeEnv.Get(Symbol.Symbol.Intern("int"));
        }

        public override ExprTy TransExpr(Env env, Stack<Level> levelStack)
        {
            Type.Type type = CheckType(env);
            Const intExpr = new Const(Value);
            return new ExprTy(type, new Ex(intExpr));
        }
    }

    public class StringExprNode : ASTExprNode
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
        public override Type.Type CheckType(Semant.Env env)
        {
            return (Type.Type)env.typeEnv.Get(Symbol.Symbol.Intern("string"));
        }

        public override ExprTy TransExpr(Env env, Stack<Level> levelStack)
        {
            Type.Type type = CheckType(env);
            // create a label
            // return label in name ir type
            // add the Frame.STRING(lab, lit) into a global list
            // The strings will be handled by the runtime
            // see page 163
            return new ExprTy(type, new Translate.DummyExpr());
        }
    }

    public class CallExprNode : ASTExprNode
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

        public override Type.Type CheckType(Env env)
        {
            FunEntry funEntry = (FunEntry)env.varEnv.Get(Symbol.Symbol.Intern(this.FuncSymbol));
            Type.Type tt = funEntry.result;
            if (tt == Symbol.Symbol.Intern("void")) throw new Exception(Error.Error.NonExistantType);
            for (int i = 0; i < funEntry.formals.Count; i++)
            {
                ASTExprNode e = this.Args[i];
                tt = e.CheckType(env);
                if (!tt.Equals(funEntry.formals[i])) throw new Exception(Error.Error.InconsistentFunc);
            }
            
            return tt;
        }

        public override ExprTy TransExpr(Env env, Stack<Level> levelStack)
        {
            Type.Type type = CheckType(env);
            LinkedList<Expr> args = new LinkedList<Expr>();
            foreach (ASTExprNode a in Args) 
            { 
                ExprTy exprTy = a.TransExpr(env, levelStack);
                args.AddLast(exprTy.expr.UnEx());
            }

            Level curLevel = levelStack.Peek();
            FunEntry fe = (FunEntry)env.varEnv.Get(Symbol.Symbol.Intern(FuncSymbol));
            Expr expr = new Temp(curLevel.Frame.GetFP());
            while (!curLevel.Equals(fe.level.Parent))
            {
                List<Access> accesses = Translate.Translate.Formals(curLevel);
                Access link = accesses.First();
                InFrame inf = (InFrame)link.fAccess;
                expr = new Mem(new BinOpExpr(BinOp.PLUS, expr, new Const(inf.Offset)));
                curLevel = curLevel.Parent;
            }

            args.AddFirst(expr);
            Call call = new Call(args, new Name(new Label(new Frame.Temp.Label(FuncSymbol))));
            return new ExprTy(type, new Ex(call));
        }
    }

    public class OpExprNode : ASTExprNode
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

        public override Type.Type CheckType(Env env)
        {
            Type.Type leftType = this.Left.CheckType(env);
            Type.Type rightType = this.Right.CheckType(env);
            if (leftType.ToString().Equals("string") && rightType.ToString().Equals("string")) {
                return leftType;
            } else if (leftType.ToString().Equals("int") && rightType.ToString().Equals("int")) {
                return leftType;
            }

            throw new Exception(Error.Error.InvalidOperation(this.Op.GetType().ToString(), leftType.ToString(), rightType.ToString()));
        }

        public override ExprTy TransExpr(Env env, Stack<Level> levelStack)
        {
            Type.Type type = CheckType(env);
            ExprTy exprTyLeft = Left.TransExpr(env, levelStack);
            ExprTy exprTyRight = Right.TransExpr(env, levelStack);
            IExpr expr = Op.TransOp(exprTyLeft.expr.UnEx(), exprTyRight.expr.UnEx());
            return new ExprTy(type, expr);
        }
    }

    public class RecordExprNode : ASTExprNode
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

        public override Type.Type CheckType(Env env)
        {
            Type.Type tt = (Type.Type)env.typeEnv.Get(Symbol.Symbol.Intern(this.TypeSymbol));
            if (tt == null) throw new Exception(Error.Error.NonExistantType);
            return tt;
        }

        public override ExprTy TransExpr(Env env, Stack<Level> levelStack)
        {
            Type.Type type = CheckType(env);
            // should also be allocated on the heap by the run time
            return new ExprTy(type, new Translate.DummyExpr());
        }
    }

    public class SeqExprNode : ASTExprNode // currently not used anywhere
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

        public override Type.Type CheckType(Semant.Env env) // not one type for this, therefore it should never be checked.
        {
            throw new Exception(Error.Error.NonExistantType);
        }

        public override ExprTy TransExpr(Env env, Stack<Level> levelStack)
        {
            Stmt seq;
            List<ExprTy> exprs = new List<ExprTy>();
            
            foreach (Sequence sequence in Sequences)
            {
                ASTExprNode expr = sequence.Expr;
                ExprTy exprty = expr.TransExpr(env, levelStack);
                exprs.Add(exprty);
            }

            if (exprs.Count == 0)
            {
                return new ExprTy(new Type.NilType(), new Nx(new Const(0)));
            }
            else if (exprs.Count == 1)
            {
                return exprs[0];
            }
            else
            {
                seq = exprs[0].expr.UnNx();
                // it may be UnEx not UnNx im not sure.
                for (int i = 1; i < exprs.Count; i++)
                {
                    seq = new Seq(seq, exprs[i].expr.UnNx());
                }
            }

            return new ExprTy(null, new Nx(seq));
        }
    }

    public class AssignExprNode : ASTExprNode
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

        public override Type.Type CheckType(Semant.Env env)
        {
            throw new Exception(Error.Error.NonExistantType);
        }

        public override ExprTy TransExpr(Env env, Stack<Level> levelStack)
        {
            Type.Type type = CheckType(env);
            return new ExprTy(type, new Translate.DummyExpr());
        }
    }

    public class IfExprNode : ASTExprNode
    {
        public ASTExprNode Test { get; }
        public ASTExprNode Then { get; }
        public ASTExprNode Else { get; }
        public IfExprNode(ASTExprNode test, ASTExprNode then_, ASTExprNode else_)
        {
            this.Test = test;
            this.Then = then_;
            this.Else = else_;
        }

        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "IfExpr {");
            Console.WriteLine(tab + "\tTest: ");
            Test.printNode(tab + "\t\t");
            Console.WriteLine(tab + "\tThen: ");
            Then.printNode(tab + "\t\t");
            if (Else != null)
            {
                Console.WriteLine(tab + "\tElse: ");
                Else.printNode(tab + "\t\t");
            }
            else
            {
                Console.WriteLine(tab + "\tElse: null");
            }

            Console.WriteLine(tab + "}");
        }

        public override Type.Type CheckType(Semant.Env env)
        {
            Type.Type t1 = this.Test.CheckType(env); // TODO: this is a bug
            Type.Type t2 = this.Then.CheckType(env);
            Type.Type t3 = this.Else.CheckType(env);
            if (t1 == t2 && t2 == t3)
            {
                return t1;
            }

            throw new Exception(Error.Error.NonExistantType);
        }

        public override ExprTy TransExpr(Env env, Stack<Level> levelStack)
        {
            Type.Type type = CheckType(env);
            Frame.Temp.Label t = new Frame.Temp.Label();
            Frame.Temp.Label f = new Frame.Temp.Label();
            Frame.Temp.Label j = new Frame.Temp.Label();
            Frame.InReg access = (Frame.InReg)levelStack.Peek().Frame.AllocLocal(false);
            Temp r = new Temp(access.Reg);

            ExprTy exprTyTest = Test.TransExpr(env, levelStack);
            ExprTy exprTyThen = Then.TransExpr(env, levelStack);
            ExprTy exprTyElse = Else.TransExpr(env, levelStack);

            CJump test = (CJump)exprTyTest.expr.UnCx()(t, f);
            Expr then = exprTyThen.expr.UnEx();
            Expr else_ = exprTyElse.expr.UnEx();

            Move thenMove = new Move(r, then);
            Move elseMove = new Move(r, else_);

            LinkedList<Label> targets = new LinkedList<Label>();
            Label join = new Label(j);
            targets.AddLast(join);
            
            Seq thenSeq = new Seq(thenMove, new Jump(new Name(join), targets)); // place these into test?
            Seq elseSeq = new Seq(elseMove, new Jump(new Name(join), targets));
            

            IExpr ifExpr = new Ex.Nx(new Seq(test, new Seq(new Seq(thenSeq, elseSeq), join))); // maybe want to change this to a Ex?
            return new ExprTy(type, ifExpr);
        }
    }

    public class WhileExprNode : ASTExprNode 
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

        public override Type.Type CheckType(Env env)
        {
            throw new Exception(Error.Error.NonExistantType);
        }

        public override ExprTy TransExpr(Env env, Stack<Level> levelStack)
        { // TODO: handle break statements see page 165
            Frame.Temp.Label preTestLabel = new Frame.Temp.Label();
            Frame.Temp.Label bodyLabel = new Frame.Temp.Label();
            Frame.Temp.Label doneLabel = new Frame.Temp.Label();
            
            ExprTy exprTyTest = Test.TransExpr(env, levelStack);
            ExprTy exprTyBody = Body.TransExpr(env, levelStack);

            LinkedList<Label> targets = new LinkedList<Label>();
            Label preTestLabelExpr = new Label(preTestLabel);
            Label bodyLabelExpr = new Label(bodyLabel);
            Label doneLabelExpr = new Label(doneLabel);
            targets.AddLast(preTestLabelExpr);
            CJump priorTest = (CJump)exprTyTest.expr.UnCx()(bodyLabel, doneLabel);
            Jump boing = new Jump(new Name(preTestLabelExpr), targets);

            Expr body = exprTyBody.expr.UnEx();
            Seq whileExpr = new Seq(preTestLabelExpr, new Seq(priorTest, new Seq(bodyLabelExpr, new Seq(body, new Seq(boing, doneLabelExpr)))));
            return new ExprTy(new Type.UnitType(), new Ex.Nx(whileExpr));
        }
    }

    public class ForExprNode : ASTExprNode
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

        public override Type.Type CheckType(Env env)
        {
            throw new Exception(Error.Error.NonExistantType);
        }

        public override ExprTy TransExpr(Env env, Stack<Level> levelStack)
        { // TODO: handle break statements see page 165
            env.varEnv.BeginScope(); // TODO: may need to add these to while as well,
                                     // or may not need at all
            env.typeEnv.BeginScope();
            Frame.Temp.Label testLabel = new Frame.Temp.Label();
            Frame.Temp.Label bodyLabel = new Frame.Temp.Label();
            Frame.Temp.Label doneLabel = new Frame.Temp.Label();

            ExprTy varExprTy = Var.TransExpr(env, levelStack);
            ExprTy lowExprTy = Lo.TransExpr(env, levelStack);
            ExprTy highExprTy = Hi.TransExpr(env, levelStack);
            ExprTy bodyExprTy = Body.TransExpr(env, levelStack);
            Expr lowExpr = lowExprTy.expr.UnEx();
            Expr highExpr = highExprTy.expr.UnEx();
            Expr varExpr = varExprTy.expr.UnEx(); // this may need to be handled differently?
            Expr bodyExpr = bodyExprTy.expr.UnEx();

            Move initVar = new Move(varExpr, lowExpr);
            CJump test = new CJump(RelOp.LE, varExpr, highExpr, testLabel, doneLabel);
            Label testIRLabel = new Label(testLabel);
            CJump postTest = new CJump(RelOp.LT, varExpr, highExpr, bodyLabel, doneLabel);
            Label bodyIRLabel = new Label(bodyLabel);
            BinOpExpr inc = new BinOpExpr(BinOp.PLUS, varExpr, new Const(1));
            LinkedList<Label> targets = new LinkedList<Label>();
            targets.AddLast(testIRLabel);
            Jump jump = new Jump(new Name(testIRLabel), targets);
            Label doneIRLabel = new Label(doneLabel);

            Seq forLoop = new Seq(initVar,
                new Seq(test,
                new Seq(testIRLabel,
                new Seq(bodyExpr,
                new Seq(postTest,
                new Seq(bodyIRLabel,
                new Seq(inc,
                new Seq(jump, doneIRLabel
                ))))))));

            env.varEnv.EndScope();
            env.typeEnv.EndScope();
            return new ExprTy(new Type.UnitType(), new Ex.Nx(forLoop));
        }
    }

    public class BreakExprNode : ASTExprNode
    {
        public int Pos { get; }
        public BreakExprNode(int pos) { this.Pos = pos; }

        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "Break {");
            Console.WriteLine(tab + "}");
        }

        public override Type.Type CheckType(Env env)
        {
            throw new Exception(Error.Error.NonExistantType);
        }

        public override ExprTy TransExpr(Env env, Stack<Level> levelStack)
        {
            Type.Type type = CheckType(env);
            // TODO: this becomes a target from a for expression
            return new ExprTy(type, new Translate.DummyExpr());
        }
    }

    public class LetExprNode : ASTExprNode
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

        public override Type.Type CheckType(Env env) 
        {
            throw new Exception(Error.Error.NonExistantType); // as a let expr is not a type/does not return one
        }

        public override ExprTy TransExpr(Env env, Stack<Level> levelStack)
        {
            env.varEnv.BeginScope();
            env.typeEnv.BeginScope();
            // I am assuming this is a set of MOVEs then the body expr
            Stmt letExpr;
            ExprTy etBody = Body.TransExpr(env, levelStack);
            if (this.Decs.Decs.Count == 0)
            {
                letExpr = etBody.expr.UnEx();
            }
            else
            {
                ASTDecNode first = this.Decs.Decs[0];
                ExprTy firstExprTy = first.TransDec(env, levelStack);
                letExpr = firstExprTy.expr.UnEx();
                if (this.Decs.Decs.Count != 1)
                {
                    foreach (ASTDecNode dec in this.Decs.Decs)
                    {
                        ExprTy et = dec.TransDec(env, levelStack);
                        if (et == null) throw new Exception(Error.Error.NonExistantType);
                        letExpr = new Seq(letExpr, et.expr.UnEx());
                    }
                }

                letExpr = new Seq(letExpr, etBody.expr.UnEx());
            }

           
            // add the decs to the environment and then discard them after the body is evaluated
            env.typeEnv.EndScope();
            env.varEnv.EndScope();
            return new ExprTy(new Type.UnitType(), new Ex.Nx(letExpr));
        }
    }

    public class ArrayExprNode : ASTExprNode
    {
        public string Sym { get; }
        public ASTExprNode Size { get; }
        public ASTExprNode Init { get; }
        public int Pos { get; }
        public ArrayExprNode(string symbol, ASTExprNode size, ASTExprNode init, int pos)
        {
            this.Sym = symbol;
            this.Size = size;
            this.Init = init;
            this.Pos = pos;
        }

        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "ArrayExpr {");
            Console.WriteLine(tab + "\tSymbol: " + this.Sym);
            Console.WriteLine(tab + "\tSize: ");
            this.Size.printNode(tab + "\t\t");
            Console.WriteLine(tab + "\tInit: ");
            this.Init.printNode(tab + "\t\t");
            Console.WriteLine(tab + "}");
        }

        public override Type.Type CheckType(Semant.Env env)
        {
            Type.Type tt = (Type.Type)env.typeEnv.Get(Symbol.Symbol.Intern(this.Sym));
            if (tt == null) throw new Exception(Error.Error.NonExistantType);
            return tt;
        }

        public override ExprTy TransExpr(Env env, Stack<Level> levelStack)
        {
            Type.Type type = CheckType(env);
            // should also be allocated on the heap by the run time, all fields are initialised to the same value unlike records
            return new ExprTy(type, new Translate.DummyExpr());
        }
    }
    
}