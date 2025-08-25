using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Frame
{
    public interface IFrame
    {
        public Temp.Label GetName();
        public List<IAccess> GetFormals();
        public IAccess AllocLocal(bool escaped);
    }
}
