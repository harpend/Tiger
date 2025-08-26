using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Frame;

namespace Tiger.Translate
{
    public class Level
    {
        public IFrame Frame { get; set; }
        public Level Parent {  get; set; }
        public List<bool> Formals {  get; set; }
        public Level(IFrame frame, Level parent, List<bool> formals)
        {
            Frame = frame;
            Parent = parent;
            Formals = formals;
        }
    }
}
