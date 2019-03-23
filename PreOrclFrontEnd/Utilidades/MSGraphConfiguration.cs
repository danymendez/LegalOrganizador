using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.Utilidades
{
    public class MSGraphConfiguration
    {
        public string CallbackPath { get; set; }
        public string Scope { get; set; }

        public string ClientId { get; set; }
        public string Tenant { get; set; }
        public string ClientSecret { get; set; }

        public string RedirectUrl { get; set; }
    }
}
