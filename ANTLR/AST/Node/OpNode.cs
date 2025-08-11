using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.ANTLR.AST.Node
{
    public abstract class ASTOpNode : ASTNode {
    }
    public class PlusOpNode : ASTOpNode {
        public override void printNode(string tab)
        {
            Console.WriteLine(tab+ "+");
        }
    }
    public class MinusOpNode : ASTOpNode {
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "-");
        }
    }
    public class TimesOpNode : ASTOpNode {
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "*");
        }
    }
    public class DivideOpNode : ASTOpNode {
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "/");
        }
    }
    public class EqOpNode : ASTOpNode {
        public override void printNode(string tab)
        {
            Console.WriteLine(tab + "=");
        }
    }
    public class NeqOpNode : ASTOpNode { 
    public override void printNode(string tab)
        {
            Console.WriteLine(tab+ "!=");
        }}
    public class LtOpNode : ASTOpNode { 
    public override void printNode(string tab)
        {
            Console.WriteLine(tab+ "<");
        }}
    public class LeOpNode : ASTOpNode { 
    public override void printNode(string tab)
        {
            Console.WriteLine(tab+ "<=");
        }}
    public class GtOpNode : ASTOpNode { 
    public override void printNode(string tab)
        {
            Console.WriteLine(tab+ ">");
        }}
    public class GeOpNode : ASTOpNode { 
    public override void printNode(string tab)
        {
            Console.WriteLine(tab+ ">=");
        }}
}
