using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Error
{
    public class SemanticError
   {
        public static string BreakPos()
        {
            return "SEMANTIC ERROR: A break expression has been found outside of a loop";
        }
    }
}
