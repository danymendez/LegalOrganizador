using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamaciasApi.Utils
{
    public class TableSerialize
    {
        public TableSerialize() {
            FieldName = new Dictionary<string, string>();
            PrimaryKeyName = new Dictionary<string, string>();
        }

        public string TableName { get; set; }
        public Dictionary<string, string> FieldName { get; set; }

        public Dictionary<string, string> PrimaryKeyName { get; set; }

        
    }
}
