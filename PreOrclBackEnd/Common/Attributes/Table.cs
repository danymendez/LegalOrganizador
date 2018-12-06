using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Entity.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class Table : Attribute
    {
        public string Name { get; set; }

    }
}
