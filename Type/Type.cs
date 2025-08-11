using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Type
{
    public class RecordField
    {
        public Symbol.Symbol name { get; set; }
        public Type type { get; set; }
        public RecordField(Symbol.Symbol name, Type type)
        {
            this.name = name;
            this.type = type;
        }
    }
    public interface Type
    {
        public string ToString();
    }

    public class IntType : Type
    {
        public override string ToString()
        {
            return "int";
        }
    }

    public class StringType : Type
    {
        public override string ToString()
        {
            return "string";
        }
    }

    public class NilType : Type
    {
        public override string ToString()
        {
            return "nil";
        }
    }

    public class UnitType : Type
    {
        public override string ToString()
        {
            return "unit";
        }
    }

    public class RecordType : Type
    {
        public List<RecordField> recordFields { get; set; } = new List<RecordField>();
        public RecordType(List<RecordField> recordFields)
        {
            this.recordFields = recordFields;
        }

        public override string ToString()
        {
            return "{" + string.Join(", ", recordFields.Select(f => $"{f.name}: {f.type}")) + "}";
        }
    }

    public class ArrayType : Type
    {
        public Type type { get; set; }
        public ArrayType(Type type)
        {
            this.type = type;
        }

        public override string ToString()
        {
            return $"{type} array";
        }
    }

    // Only needed if supporting recursive types
    //public class NameType : Type
    //{
    //    public Symbol.Symbol symbol { get; set; }
    //    public NameType(Symbol.Symbol symbol)
    //    {
    //        this.symbol = symbol;
    //    }
    //}
}
