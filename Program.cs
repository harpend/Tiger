using Antlr4.Runtime;
using Tiger.ANTLR;
using Tiger.ANTLR.AST;

namespace Tut;

public class Program
{
    static void Main(string[] args)
    {
        var fileContents = File.ReadAllText("./Tests/test1.tgr");
        AntlrInputStream stream = new AntlrInputStream(fileContents);
        TigerLexer tigerLexer = new TigerLexer(stream);
        CommonTokenStream tokens = new CommonTokenStream(tigerLexer);
        TigerParser parser = new TigerParser(tokens);
        TigerParser.ProgramContext program = parser.program();
        TigerVisitor visitor = new TigerVisitor(program);

    }
}