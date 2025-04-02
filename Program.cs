using Antlr4.Runtime;
using Tiger.ANTLR;
using Tiger.ANTLR.AST;
using Tiger.ANTLR.AST.Node;
using Tiger.Table;

namespace Tut;

public class Program
{
    public static TypeTable typeTable;
    public static SymbolTable symbolTable;
    static void Main(string[] args)
    {
        var fileContents = File.ReadAllText("C:/Users/jackt/OneDrive/Documents/Compilers/Tiger/Tiger/Tests/test4.tgr");
        AntlrInputStream stream = new AntlrInputStream(fileContents);
        TigerLexer tigerLexer = new TigerLexer(stream);
        CommonTokenStream tokens = new CommonTokenStream(tigerLexer);
        TigerParser parser = new TigerParser(tokens);
        parser.RemoveErrorListeners(); // Remove default console error listener
        var errorListener = new CustomErrorListener();
        parser.AddErrorListener(errorListener);
        TigerParser.ProgramContext program = parser.program();
        //Console.WriteLine(program.ToStringTree());
        if (parser.NumberOfSyntaxErrors > 0)
        {
            Console.WriteLine("Parsing failed. Check syntax errors.");
            return;
        }
        TigerVisitor visitor = new TigerVisitor();
        typeTable = new TypeTable();
        symbolTable = new SymbolTable();
        ASTNode ast = visitor.Visit(program);
        if (ast == null)
        {
            Console.WriteLine("AST is null");
        } else
        {
        PrintAST(ast);

        }
    }

    static void PrintAST(ASTNode node, int indent = 0)
    {
        if (!(node is ProgramNode))
        {
            Console.WriteLine("Unexpected ASTNode");
        }

        node.printNode("");
    }

    public class CustomErrorListener : BaseErrorListener
    {
        public bool HasErrors { get; private set; } = false;

        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol,
                                         int line, int charPositionInLine, string msg, RecognitionException e)
        {
            HasErrors = true;
            Console.WriteLine($"Syntax Error at line {line}, column {charPositionInLine}: {msg}");
        }
    }
}