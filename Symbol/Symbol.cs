using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Symbol
{
    public class Symbol // Could just use a string, but this is more efficient for comparisons.
    {
        private static Dictionary<string, Symbol> table = new Dictionary<string, Symbol>();
        public string name { get; set; }
        public Symbol(string name)
        {
            this.name = name;
        }

        public static Symbol Intern(string name)
        {
            if (table.TryGetValue(name, out Symbol symbol))
            {
                return symbol;
            }
            else
            {
                symbol = new Symbol(name);
                table[name] = symbol;
                return symbol;
            }
        }
    }
}
