using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.ANTLR.AST.Node
{
    abstract class ASTOpNode : ASTNode { }
    class PlusOpNode : ASTOpNode {
        public override void printNode(string tab)
        {
            Console.WriteLine(tab+ "+");
        }
    }
    class MinusOpNode : ASTOpNode {
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "-");
        }
    }
    class TimesOpNode : ASTOpNode {
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "*");
        }
    }
    class DivideOpNode : ASTOpNode {
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "/");
        }
    }
    class EqOpNode : ASTOpNode {
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "=");
        }
    }
    class NeqOpNode : ASTOpNode { 
    public override void printNode(string tab)
        {
            Console.WriteLine(tab+ "!=");
        }}
    class LtOpNode : ASTOpNode { 
    public override void printNode(string tab)
        {
            Console.WriteLine(tab+ "<");
        }}
    class LeOpNode : ASTOpNode { 
    public override void printNode(string tab)
        {
            Console.WriteLine(tab+ "<=");
        }}
    class GtOpNode : ASTOpNode { 
    public override void printNode(string tab)
        {
            Console.WriteLine(tab+ ">");
        }}
    class GeOpNode : ASTOpNode { 
    public override void printNode(string tab)
        {
            Console.WriteLine(tab+ ">=");
        }}
}
