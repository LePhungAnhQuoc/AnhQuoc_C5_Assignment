using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Utilities.CheckProperty
{
    public class IsCheckProperties
    {
        public CheckPropertyType Type { get; set; }
        public List<string> ListProperties { get; set; }

        public IsCheckProperties(params string[] listProperties)
        {
            Type = CheckPropertyType.Include;
            ListProperties = new List<string>(listProperties);
        }
        public IsCheckProperties(CheckPropertyType type, params string[] listProperties) : this(listProperties)
        {
            Type = type;
        }
    }
}
