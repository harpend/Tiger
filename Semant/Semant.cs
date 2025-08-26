using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.ANTLR.AST.Node;
using Tiger.Translate;

namespace Tiger.Semant
{
    public class Semant
    {
        private Env env = new Env();
        private Stack<Level> levelStack = new Stack<Level>();
        public Semant()
        {
            env = new Env();
            levelStack.Push(Translate.Translate.Outermost); // represents the global level
        }

        public List<ExprTy> TransProg(ProgramNode pn)
        {
            return pn.TransProg(env, levelStack);
        }
        public ExprTy TransVar(ASTVarNode var)
        {
            return var.TransVar(env, levelStack);
        }

        public ExprTy TransExpr(ASTExprNode expr)
        {
            return expr.TransExpr(env, levelStack);
        }

        public void TransDec(ASTDecNode dec)
        {
            dec.TransDec(env, levelStack); // TODO: TransDec should handle levels as well
        }

        public Type.Type TransType(ASTTypeNode typeNode)
        {
            switch (typeNode)
            {
                case ArrayTypeNode arrayTypeNode:
                    return new Type.ArrayType(TransType(arrayTypeNode.Sym));
                case RecordTypeNode recordTypeNode:
                    List<Type.RecordField> fields = new List<Type.RecordField>();
                    foreach (Field field in recordTypeNode.Fields)
                    {
                        Type.Type type = TransType(field.TypeSymbol);
                        fields.Add(new Type.RecordField(Symbol.Symbol.Intern(field.NameSymbol), type));
                    }

                    return new Type.RecordType(fields);
                case NameTypeNode nameTypeNode:
                    return TransType(nameTypeNode.Sym);
            }

            throw new Exception(Error.Error.NonExistantType);
        }

        public Type.Type TransType(string typeName)
        {
            Type.Type type = (Type.Type)env.typeEnv.Get(Symbol.Symbol.Intern(typeName));
            if (type == null)
            {
                throw new Exception(Error.Error.NonExistantType);
            }

            return type;
        }

        // only to be used for testing
        public Env GetEnv()
        {
            return env;
        }
    }
}
