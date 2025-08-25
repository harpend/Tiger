using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Frame.Arch
{
    // Frame Structure:
    // return address (via call instruction caller)
    // saved RBP (pushed by callee)
    // calee saved regs
    // local/escape slots

    public class x64Frame : IFrame
    {
        private int Offset = 0;
        
        // fized regs
        private Temp.Temp RBP = new Temp.Temp();
        private Temp.Temp RSP = new Temp.Temp();
        private Temp.Temp RAX = new Temp.Temp();

        private Temp.Temp[] argRegs = new Temp.Temp[] { 
            new Temp.Temp(), // RDI
            new Temp.Temp(), // RSI
            new Temp.Temp(), // RDX
            new Temp.Temp(), // RCX
            new Temp.Temp(), // R8
            new Temp.Temp()  // R9
        };

        private Temp.Label Name {  get; set; }
        private List<IAccess> Formals { get; set; }
        public x64Frame(Temp.Label label, List<bool> formals)
        {
            if (formals.Count > 6)
            {
                // this means we don't have to worry about parameters getting passed on the stack 
                throw new ArgumentException("too many parameters");
            }

            Name = label;
            Formals = new List<IAccess>();
            for (int i = 0; i < formals.Count; i++) { 
                bool escaped = formals[i];
                if (escaped)
                {
                    // TODO: add a mov instruction for the corresponding arg reg
                    InFrame local = new InFrame(Offset);
                    Offset -= 8;
                    Formals.Add(local);
                } else
                {
                    InReg r = new InReg(argRegs[i]);  
                    Formals.Add(r);
                }
            }
        }

        public Temp.Label GetName() { return Name; }
        public List<IAccess> GetFormals() { return Formals; }
        public IAccess AllocLocal(bool escaped)
        {
            if (escaped)
            {
                InFrame local = new InFrame(Offset);
                Offset -= 8;
                return local;
            } else
            {
                return new InReg();
            }
        }
    }
}
