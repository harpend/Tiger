using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tiger.Frame;
using Tiger.Translate;

namespace Tiger.Table
{
    public interface IEntry
    {
        public string name { get; }
    }

    public class VarSym : IEntry
    {
        public string name { get; }
        public TigerType type { get; }
        public Access access { get; }
        public VarSym(string name, TigerType type, Access access)
        {
            this.name = name;
            this.type = type;
            this.access = access;
        }
    }

    public class FuncSym : IEntry
    {
        public string name { get; }
        public List<TigerType> parameters { get; }
        public TigerType retType { get; }
        public Level level;
        public Label label;
        public FuncSym(string name, List<TigerType> parameters, TigerType retType, Level level, Label label)
        {
            this.name = name;
            this.parameters = parameters;
            this.retType = retType;
            this.level = level;
            this.label = label;
        }
    }
    public class Symbol // finish separating into new IEntry for var and func
    {
        public IEntry entry;
        private static readonly ImmutableDictionary<string, Symbol>.Builder dict = ImmutableDictionary.CreateBuilder<string, Symbol>();
        public override String ToString() { return entry.name; }

        public bool IsFunction()
        {
            return entry is FuncSym;
        }
        private Symbol(string name, TigerType type, Access access)
        {
            this.entry = new VarSym(name, type, access);
        }
        private Symbol(string name, TigerType type, List<TigerType> prms, TigerType ret, Label label, Level level) 
        {
            this.entry = new FuncSym(name, prms, ret, level, label);
        }

        public static Symbol symbol(string name, TigerType type, Access access)
        {
            string u = string.Intern(name);
            if (!dict.TryGetValue(u, out Symbol s))
            {
                s = new Symbol(u, type, access);
                dict[u] = s;
            }
            return s;
        }
        public static Symbol fnSymbol(string name, List<TigerType> prms, TigerType ret, Label label, Level level)
        {
            string u = string.Intern(name);
            if (!dict.TryGetValue(u, out Symbol s))
            {
                TigerType type = TigerType.type("function");
                s = new Symbol(u, type, prms, ret, label, level);
                dict[u] = s;
            }
            return s;
        }
    }
}
