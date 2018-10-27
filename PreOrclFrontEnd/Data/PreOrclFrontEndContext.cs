using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Common.Entity.Models;

namespace PreOrclFrontEnd.Models
{
    public class PreOrclFrontEndContext : DbContext
    {
        public PreOrclFrontEndContext (DbContextOptions<PreOrclFrontEndContext> options)
            : base(options)
        {
        }

        public DbSet<Common.Entity.Models.SisPerPersona> SisPerPersona { get; set; }
    }
}
