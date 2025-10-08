using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Frame.Temp
{
    public class Temp
    {
        public static int GlobalCount = 40;
        public int Count;
        public Temp()
        {
            Count = GlobalCount;
            GlobalCount++;
        }

        public Temp(int t)
        {
            if (t < 40)
            {
                Count = t;
            } else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
