using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Error
{
    public static class Error
    {
        public static string BreakError = "Break found outside a loop.";
        public static string NonExistantType = "No type could be resolved.";
        public static string InconsistentFunc = "Inconsistent function params.";
        public static string InconsistentType = "Inconsistent types.";
        public static string NilType = "Cannot declare non-record nil type.";
        public static string InvalidOperation(string operation, string type1, string type2)
        {
            return $"Invalid operation '{operation}' between types '{type1}' and '{type2}'.";
        }
    }
}
