using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamaciasApi.Attributes
{

    public class PrimaryKey : Attribute
    {
        public string FieldName { get; set; }
    }
}
