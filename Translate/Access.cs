using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Translate
{
    public class Access
    {
        public Level level {  get; set; }
        public Frame.IAccess fAccess {  get; set; }
        public Access(Level level, Frame.IAccess fAccess)
        {
            this.level = level;
            this.fAccess = fAccess;
        }
    }
}
