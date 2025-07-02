using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Frame
{
    // Activation Record: contains local variables, formal parameters, return address and other temps.
    public interface IFrame
    {
        public IFrame NewFrame(Label name, LinkedList<bool> formals);
        public Label Name();
        public List<IAccess> Formals();
        public IAccess AllocLocal(bool formal);
        public IR.Stmt ProcEntryExit1(IR.Stmt body);
        public IR.Stmt ProcEntryExit2(IR.Stmt body);
        public IR.Stmt ProcEntryExit3(IR.Stmt body);
        public Temp RetVal();
    }

    public interface IAccess { }

    public class InFrame : IAccess
    {
        public IFrame frame;
        public int offset { get; } // this is the offset from fp
        public InFrame(IFrame frame, int offset)
        {
            this.offset = offset;
        }
    }

    public class InReg : IAccess
    {
        public Temp temp { get; }
        public InReg(Temp temp)
        {
            this.temp = temp;
        }
    }

    public class Label // assembly label
    {
        static int count = 0;
        public string name { get; }
        public Label()
        {
            this.name = "L" + count++;
        }
    }

    public class Temp
    {
        static int count = 0;
        public int id;
        public Temp()
        {
            this.id = count++;
        }

        public Temp(int i)
        {
            this.id = i;
        }
    }

    public interface Frag { }

    public class Proc : Frag
    {
        public IR.Stmt stmt;
        public IFrame frame;
        public Proc(IR.Stmt stmt, IFrame frame)
        {
            this.stmt = stmt;
            this.frame = frame;
        }
    }

    public class String : Frag
    {
        public Label label;
        public string str;
        public String(Label label, string str)
        {
            this.label = label;
            this.str = str;
        }
    }
}
