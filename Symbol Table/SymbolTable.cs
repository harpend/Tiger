using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Symbol_Table
{
    public class SymbolTable 
    { 
        private readonly ImmutableSortedDictionary<Symbol, object> dict;
        public SymbolTable() {
            this.dict = ImmutableSortedDictionary<Symbol, object>.Empty;
        }
        
        private SymbolTable(ImmutableSortedDictionary<Symbol, object> bindings) { 
            this.dict = bindings;
        }
        public SymbolTable Update(IEnumerable<KeyValuePair<Symbol, object>> items) { // overwrites if already exists
            return new SymbolTable(this.dict.SetItems(items));
        }
        public SymbolTable AddTable(SymbolTable super)
        {
            return new SymbolTable(this.dict.SetItems(super.dict));
        }

        public object Get(Symbol key)
        {
            return this.dict.TryGetValue(key, out var value) ? value : null;
        }

    }
}
