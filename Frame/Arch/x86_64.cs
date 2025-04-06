using System;
using System.Collections.Generic;
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
        public IFrame NewFrame(Label name, List<bool> formals)
        {

        }

        public Label Name()
        {

        }
        public List<IAccess> Formals()
        {
           
        }
        public IAccess AllocLocal(bool formal)
        {

        }
    }
}
