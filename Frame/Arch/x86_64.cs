using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    x86_64 Windows Stack Frame Layout (Microsoft x64 Calling Convention)
    ----------------------------------------------------------------------

    Stack grows downward (from high to low addresses)

    +-------------------------------+ <-- Higher memory
    | Return address                | <- pushed by CALL instruction
    +-------------------------------+
    | Optional stack-based arguments| <- if more than 4 parameters
    +-------------------------------+
    | Shadow space (32 bytes)       | <- required space for 4 arguments
    | (4 x 8 bytes for RCX, RDX,    |
    |  R8, R9 even if not used)     |
    +-------------------------------+
    | Saved registers (callee-saved)|
    | e.g., RBX, RBP, R12–R15       |
    +-------------------------------+
    | Local variables               |
    | (aligned to 16 bytes)         |
    +-------------------------------+
    | Stack alignment padding       |
    +-------------------------------+
    | RSP points here after prologue|
    +-------------------------------+ <-- Lower memory

    Notes:
    - First 4 integer/pointer args: RCX, RDX, R8, R9
    - Floating-point args: XMM0–XMM3 // not using
    - Additional arguments: passed on stack (above shadow space) // not using
    - Return value: RAX or XMM0 // not using XMM0
    - Callee must allocate *shadow space* of 32 bytes (mandatory)
    - Stack must be 16-byte aligned before a CALL
    - Registers that must be preserved by the callee:
        RBX, RBP, RDI, RSI, R12–R15

    Typical Function Prologue:
        sub rsp, N          ; allocate local vars + shadow space
        mov [rsp + ...], rbx  ; save callee-saved registers

    Typical Function Epilogue:
        mov rbx, [rsp + ...]  ; restore callee-saved registers
        add rsp, N
        ret
*/

namespace Tiger.Frame.Arch
{
    // Holds the following:
    // Locations of all the formals (parameters)
    // instructions required to implement the view shift
    // label at which the functions machine code begins
    class x86_64 : IFrame
    {
        private int offset = 0; // in bytes
        public Label name;
        public List<IAccess> formals; // RCX, RDX, R8, R9
        public List<IAccess> locals;
        public IFrame NewFrame(Label name, LinkedList<bool> formals) 
        {
            if (formals.Count > 4) // first 4 passed in registers any more should be InFrame
            {
                throw new Exception("too many formals");
            }

            List<IAccess> accesses = new List<IAccess>();
            foreach (var formal in formals)
            {
                // need to consider later what to do if a formal escapes
                accesses.Add(new InReg(new Temp())); // assumes all are false     
            }

            return new x86_64(name, accesses);
        }

        public x86_64()
        {
            this.name = null;
            this.formals = null;
        }

        private x86_64(Label name, List<IAccess> formals)
        {
            this.name = name;
            this.formals = formals;
        }

        public Label Name()
        {
            return name;
        }
        public List<IAccess> Formals()
        {
           return this.formals;
        }
        public IAccess AllocLocal(bool escape)
        {
            if (escape)
            {
                InFrame inFrame = new InFrame(this, offset);
                locals.Add(inFrame);
                offset -= 8;
                return inFrame;
            }

            InReg inReg = new InReg(new Temp());
            locals.Add(inReg);
            return inReg;
        }
    }
}
