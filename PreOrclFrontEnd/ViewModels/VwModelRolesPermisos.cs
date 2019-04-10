using PreOrclFrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.ViewModels
{
    public class VwModelRolesPermisos : Roles
    {
       public List<Permisos> Permisos { get; set; }
    }
}
