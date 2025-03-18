using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Error
{
    public class TypeError
    {
        public static string Undeclared(string symbol)
        {
            return "TYPE ERROR: Type " + symbol + " has not been declared";
        }
        
        public static string Mismatched(string type1, string type2)
        {
            return "TYPE ERROR: Type " + type1 + " cannot be evaluated to Type " + type2;
        }
        public static string InvalidOperation(string type1, string type2)
        {
            return "TYPE ERROR: Type " + type1 + " cannot be op'd to Type " + type2;
        }
    }
}
