using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Frame
{
    public class InReg : IAccess
    {
        Temp.Temp Reg {  get; set; }
        public InReg() { 
            Reg = new Temp.Temp();
        }

        public InReg(Temp.Temp reg)
        {
            Reg = reg;
        }
    }
}
