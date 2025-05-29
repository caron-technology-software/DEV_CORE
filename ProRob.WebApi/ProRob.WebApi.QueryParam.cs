using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProRob.WebApi
{
    public class QueryParam
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public QueryParam(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public static QueryParam Build(string name, object value)
        {
            return new QueryParam(name, value);
        }
    }
}
