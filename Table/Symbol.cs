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
        private TigerType type;
        private static readonly ImmutableDictionary<string, Symbol>.Builder dict = ImmutableDictionary.CreateBuilder<string, Symbol>();
        public override String ToString() { return name; }

        private Symbol(string name, TigerType type)
        {
            this.name = name;
            this.type = type;
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
    }
}
