using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Frame
{
    // holds information about formal parameters and variables located in the frame
    public interface IFrame
    {
        public IFrame NewFrame(Label name, List<bool> formals);
        public Label Name();
        public List<IAccess> Formals();
        public IAccess AllocLocal(bool formal);
    }

    public interface IAccess { }

    public class InFrame : IAccess
    {
        public int offset { get; } // this is the offset from fp
        public InFrame(int offset)
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

    public class Label
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
    }
}
