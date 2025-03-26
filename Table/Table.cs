using Antlr4.Runtime.Atn;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Table
{
    public class SymbolTable 
    { 
        private readonly ImmutableSortedDictionary<string, Symbol> dict;
        public SymbolTable() {
            this.dict = ImmutableSortedDictionary<string, Symbol>.Empty;
        }
        
        private SymbolTable(ImmutableSortedDictionary<string, Symbol> bindings) { 
            this.dict = bindings;
        }
        public SymbolTable Update(IEnumerable<KeyValuePair<string, Symbol>> items) { // overwrites if already exists
            return new SymbolTable(this.dict.SetItems(items));
        }
        public SymbolTable AddTable(SymbolTable super)
        {
            return new SymbolTable(this.dict.SetItems(super.dict));
        }
        public SymbolTable PutFn(string key, List<string> prms, string ret, TypeTable tt) // add other requirements later on such as scope
        {
            if (!tt.Exists(ret)) throw new Exception("Type doesn't exist");
            List<TigerType> prmtypes = new List<TigerType>();
            foreach (string prm in prms)
            {
                if (!tt.Exists(prm)) throw new Exception("Type doesn't exist");
                prmtypes.Add(TigerType.type(prm));
            }
            TigerType typ = TigerType.type(ret);
            Symbol symbol = Symbol.fnSymbol(key, prmtypes, typ);
            return new SymbolTable(dict.SetItem(key, symbol));
        }
        public SymbolTable Put(string key, string type, TypeTable tt) // add other requirements later on such as scope
        {
            if (!tt.Exists(type)) throw new Exception("Type doesn't exist");
            TigerType typ = TigerType.type(type);
            Symbol symbol = Symbol.symbol(key, typ);
            return new SymbolTable(dict.SetItem(key, symbol));
        }
        public Symbol? Get(string key)
        {
            return this.dict.TryGetValue(key, out var value) ? value : null;
        }

    }
    
    public class TypeTable 
    { 
        private readonly ImmutableSortedDictionary<string, TigerType> dict;
        public TypeTable() {
            this.dict = ImmutableSortedDictionary<string, TigerType>.Empty
                .Add("int", TigerType.type("int"))
                .Add("string", TigerType.type("string"))
                .Add("void", TigerType.type("void"))
                .Add("nil", TigerType.type("nil"));
        }
        
        private TypeTable(ImmutableSortedDictionary<string, TigerType> bindings) { 
            this.dict = bindings;
        }
        public TypeTable Update(IEnumerable<KeyValuePair<string, TigerType>> items) { // overwrites if already exists
            return new TypeTable(this.dict.SetItems(items));
        }
        public TypeTable AddTable(TypeTable super)
        {
            return new TypeTable(this.dict.SetItems(super.dict));
        }

        public TypeTable Put(string key)
        {
            TigerType typ = TigerType.type(key);
            return new TypeTable(dict.SetItem(key, typ));
        }
        public bool Exists(string key)
        {
            return dict.ContainsKey(key);
        } 
        public TigerType? Get(string key)
        {     
            return this.dict.TryGetValue(key, out var value) ? value : null;
        }

    }
}
