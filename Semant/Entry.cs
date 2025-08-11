using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Semant
{
    internal interface Entry
    {

    }

    internal class VarEntry : Entry
    {
        public Type.Type type { get; set; }
        public VarEntry(Type.Type type)
        {
            this.type = type;
        }
    }

    internal class FunEntry : Entry
    {
        public List<Type.Type> formals { get; set; }
        public Type.Type result { get; set; }
        public FunEntry(List<Type.Type> formals, Type.Type result)
        {
            this.formals = formals;
            this.result = result;
        }
    }
}
