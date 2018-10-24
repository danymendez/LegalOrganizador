using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Attributes
{

    public class PrimaryKey : Attribute
    {
        public bool AutoIncrement { get; set; }
    }
}
