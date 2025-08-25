using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Frame
{
    public class InFrame : IAccess
    {
        public int Offset { get; }
        public InFrame(int offset)
        {
            Offset = offset;
        }
    }
}
