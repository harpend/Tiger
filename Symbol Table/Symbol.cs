using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Symbol_Table
{
    public class Symbol
    {
        private string name;
        private static readonly ImmutableDictionary<string, Symbol>.Builder dict = ImmutableDictionary.CreateBuilder<string, Symbol>();
        public override String ToString() { return name; }

        private Symbol(string name)
        {
            this.name = name;
        }

        public static Symbol symbol(string name)
        {
            string u = string.Intern(name);
            if (!dict.TryGetValue(u, out Symbol s))
            {
                s = new Symbol(u);
                dict[u] = s;
            }
            return s;
        }
    }
}
