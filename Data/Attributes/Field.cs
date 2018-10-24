using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Attributes
{
    public class Field : Attribute
    {
        public string Name { get; set; }
    }
}
