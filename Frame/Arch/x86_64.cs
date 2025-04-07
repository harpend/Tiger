using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Frame.Arch
{
    // Holds the following:
    // Locations of all the formals (parameters)
    // instructions required to implement the view shift
    // label at which the functions machine code begins
    class x86_64 : IFrame
    {
        public Label name;
        public List<IAccess> formals;
        public IFrame NewFrame(Label name, LinkedList<bool> formals)
        {
            List<IAccess> accesses = new List<IAccess>();
            foreach (var formal in formals)
            {
                if (formal)
                {
                    accesses.Add(AllocLocal(formal));
                }
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
           
        }
        public IAccess AllocLocal(bool formal)
        {

        }
    }
}
