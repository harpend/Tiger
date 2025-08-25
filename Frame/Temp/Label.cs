using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Frame.Temp
{
    public class Label
    {
        public static int GlobalCount = 0;
        public string Name;
        public Label()
        {
            Name = "L" + GlobalCount;
            GlobalCount++;
        }
    }
}
