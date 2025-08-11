using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Semant
{
    public class Env
    {
        public Table.Table typeEnv { get; set; }
        public Table.Table varEnv { get; set; }

        public Env()
        {
            typeEnv = baseTypeEnv();
            varEnv = baseVarEnv();
        }

        private Table.Table baseTypeEnv()
        {
            Table.Table table = new Table.Table();
            table.Push(Symbol.Symbol.Intern("int"), new Type.IntType());
            table.Push(Symbol.Symbol.Intern("string"), new Type.StringType());
            table.Push(Symbol.Symbol.Intern("nil"), new Type.NilType());
            //table.Push(Symbol.Symbol.Intern("unit"), new Type.UnitType());
            return table;
        }

        private Table.Table baseVarEnv()
        {
            Table.Table table = new Table.Table();
            // TODO: place predefined functions here in the following form
            // table.Push(Symbol.Symbol.Intern("example"), new FunEntry(formals, result));
            return table;
        }
    }
}
