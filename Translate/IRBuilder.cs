using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Tiger.Frame;
using Tiger.IR;

namespace Tiger.Translate
{
    public class IRBuilder
    {
        public Frame.IFrame frame;
        public LinkedList<Frag> frags;
        public IRBuilder(IFrame frame)
        {
            this.frame = frame;
            this.frags = new LinkedList<Frag>();
        }
        public void procEntryExit(Level level, Converters.IExpr body, bool rv)
        {
            Stmt b = null;
            if (rv) // need to check if there is a return value and store it in correct register
            {
                b = new Move(new IR.Temp(level.frame.RetVal()), body.UnEx());
            } else {
                b = body.UnNx();
            }

            b = level.frame.ProcEntryExit1(b);
            frags.AddLast(new Proc(b, level.frame));
        }

        public LinkedList<Frame.Frag> GetResult()
        {
           
        }
    }
}
