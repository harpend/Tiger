using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Frame.Temp;
using Tiger.Translate;

namespace Tiger.Semant
{
    public interface Entry
    {

    }

    public class VarEntry : Entry
    {
        public Type.Type type { get; set; }
        public Translate.Access access { get; set; }
        public VarEntry(Type.Type type, Translate.Access access)
        {
            this.type = type;
            this.access = access;
        }
    }

    public class FunEntry : Entry
    {
        public List<Type.Type> formals { get; set; }
        public Type.Type result { get; set; }
        public Label label { get; set; }
        public Level level { get; set; }
        public FunEntry(List<Type.Type> formals, Type.Type result, Label label, Level level)
        {
            this.formals = formals;
            this.result = result;
            this.label = label;
            this.level = level;
        }
    }
}
