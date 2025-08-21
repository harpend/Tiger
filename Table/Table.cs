using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Table
{
    public class Binding
    {
        public Symbol.Symbol symbol { get; set; }
        public object value { get; set; }
        public Binding prevBinding { get; set; }

        public Binding(Symbol.Symbol symbol, object value, Binding prevBinding = null)
        {
            this.symbol = symbol;
            this.value = value;
            this.prevBinding = prevBinding;
        }
    }

    // Hashtable that if a key is overwritten contains information to restore
    public class Table
    {
        private Dictionary<Symbol.Symbol, Binding> bindings = new Dictionary<Symbol.Symbol, Binding>();
        private Stack<Binding> curSymbols = new Stack<Binding>(); // symbols of the current scope
        private Binding mark = new Binding(null, null); // mark for the current scope
        public Table()
        {

        }

        public void Push(Symbol.Symbol s, object v)
        {
            Binding previousBinding = bindings.ContainsKey(s) ? bindings[s] : null;
            Binding b = new Binding(s, v, previousBinding);
            bindings.Add(s, b);
            curSymbols.Push(b);
        }

        public object Get(Symbol.Symbol s)
        {
            if (!bindings.ContainsKey(s))
            {
                throw new KeyNotFoundException($"Symbol {s.name} not found in the table.");
            }

            Binding b = bindings[s];
            return b.value;
        }

        public void BeginScope()
        {
            mark = new Binding(null, null, mark);
            curSymbols.Push(mark);
        }

        public void EndScope()
        {
            if (mark.prevBinding == null)
            {
                throw new InvalidOperationException("No scope to end");
            } 

            while (curSymbols.Count > 0 && curSymbols.Peek() != mark)
            {
                Binding b = curSymbols.Pop();
                if (b.prevBinding != null)
                {
                    bindings[b.symbol] = b.prevBinding;
                } else
                {
                    bindings.Remove(b.symbol);
                }
            }

            curSymbols.Pop();
            mark = mark.prevBinding;
        }
    }
}
