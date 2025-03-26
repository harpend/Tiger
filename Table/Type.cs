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
        public string typeName { get; }
        private static readonly ImmutableDictionary<string, TigerType>.Builder dict = ImmutableDictionary.CreateBuilder<string, TigerType>();
        public override String ToString() { return typeName; }
        public bool isRecord { get; }
        public Dictionary<string, TigerType> fields { get; }
        private TigerType(string type)
        {
            this.typeName = type;
            this.isRecord = false;
        }

        private TigerType(string type, Dictionary<string, TigerType> fields)
        {
            this.typeName = type;
            this.fields = fields;
            this.isRecord = true;
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
        public static TigerType recordType(string tigerType, List<string> fields)
        {
            string u = string.Intern(tigerType);
            if (!dict.TryGetValue(u, out TigerType s))
            {
                Dictionary<string, TigerType> typeDict = new Dictionary<string, TigerType>();
                foreach (string f in fields)
                {
                    if (!dict.TryGetValue(f, out TigerType t)) throw new Exception(Error.TypeError.NonExistantType());
                    typeDict.Add(f, t);
                }

                s = new TigerType(u, typeDict);
                dict[u] = s;
            }

            return s;
        }
    }
}
