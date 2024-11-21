using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SOA_CA2_Cian_Nojus.Models;

namespace SOA_CA2_Cian_Nojus.Data
{
    public class SOA_CA2_Cian_NojusContext : DbContext
    {
        public SOA_CA2_Cian_NojusContext (DbContextOptions<SOA_CA2_Cian_NojusContext> options)
            : base(options)
        {
        }

        public DbSet<SOA_CA2_Cian_Nojus.Models.Games> Games { get; set; } = default!;
        public DbSet<SOA_CA2_Cian_Nojus.Models.Developer> Developer { get; set; } = default!;
        public DbSet<SOA_CA2_Cian_Nojus.Models.Platform> Platform { get; set; } = default!;
    }
}
