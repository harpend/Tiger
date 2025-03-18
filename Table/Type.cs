using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tiger.Table
{
    public class TigerType
    {
        private string typeName;
        private static readonly ImmutableDictionary<string, TigerType>.Builder dict = ImmutableDictionary.CreateBuilder<string, TigerType>();
        public override String ToString() { return typeName; }

        private TigerType(string type)
        {
            this.typeName = type;
        }

        public static TigerType type(string tigerType)
        {
            string u = string.Intern(tigerType);
            if (!dict.TryGetValue(u, out TigerType s))
            {
                s = new TigerType(u);
                dict[u] = s;
            }
            return s;
        }
    }
}
