using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PreOrclApi.Models
{
    public class PreOrclApiContext : DbContext
    {
        public PreOrclApiContext (DbContextOptions<PreOrclApiContext> options)
            : base(options)
        {
        }

        public DbSet<PreOrclApi.Models.SisPerPersona> SisPerPersona { get; set; }
    }
}
