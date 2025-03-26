using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Table
{
    public class Symbol
    {
        private string name;
        public TigerType type { get; }
        public bool isFunction { get; }
        public List<TigerType> parameters { get; }
        public TigerType retType { get; }
        private static readonly ImmutableDictionary<string, Symbol>.Builder dict = ImmutableDictionary.CreateBuilder<string, Symbol>();
        public override String ToString() { return name; }

        private Symbol(string name, TigerType type)
        {
            this.name = name;
            this.type = type;
            this.isFunction = false;
            this.parameters = null;
            this.retType = null;
        }
        private Symbol(string name, TigerType type, List<TigerType> prms, TigerType ret) 
        {
            this.name = name;
            this.type = type;
            this.isFunction = true;
            this.parameters = prms;
            this.retType = ret;
        }

        public static Symbol symbol(string name, TigerType type)
        {
            string u = string.Intern(name);
            if (!dict.TryGetValue(u, out Symbol s))
            {
                s = new Symbol(u, type);
                dict[u] = s;
            }
            return s;
        }
        public static Symbol fnSymbol(string name, List<TigerType> prms, TigerType ret)
        {
            string u = string.Intern(name);
            if (!dict.TryGetValue(u, out Symbol s))
            {
                TigerType type = TigerType.type("function");
                s = new Symbol(u, type, prms, ret);
                dict[u] = s;
            }
            return s;
        }
    }
}
