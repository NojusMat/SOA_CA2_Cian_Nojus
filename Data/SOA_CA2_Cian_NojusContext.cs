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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Games>().HasData(
                new Games
                {
                    Id = 1,
                    title = "Halo",
                    genre = "Shooter",
                    release_year = 2001,
                    developer_id = 1,
                },
                new Games
                {
                    Id = 2,
                    title = "Gears of War",
                    genre = "Shooter",
                    release_year = 2006,
                    developer_id = 1,
                },
                new Games
                {
                    Id = 3,
                    title = "Forza Horizon",
                    genre = "Racing",
                    release_year = 2023,
                    developer_id = 1,
                },
                new Games
                {
                    Id = 4,
                    title = "The Legend of Zelda: Breath of the Wild",
                    genre = "Action-Adventure",
                    release_year = 2024,
                    developer_id = 2,
                },
                new Games
                {
                    Id = 5,
                    title = "Super Mario Odyssey",
                    genre = "Platformer",
                    release_year = 2008,
                    developer_id = 2,
                },
                new Games
                {
                    Id = 6,
                    title = "Mario Kart 8 Deluxe",
                    genre = "Racing",
                    release_year = 2020,
                    developer_id = 2,
                }
            );

            modelBuilder.Entity<Developer>().HasData(
                new Developer
                {
                    Id = 1,
                    name = "Microsoft",
                    country = "USA"
                },
                new Developer
                {
                    Id = 2,
                    name = "Nintendo",
                    country = "Japan"
                }
            );

            modelBuilder.Entity<Platform>().HasData(
                new Platform
                {
                    Id = 1,
                    name = "Xbox",
                    manufacturer = "Microsoft"
                },
                new Platform
                {
                    Id = 2,
                    name = "Nintendo Switch",
                    manufacturer = "Nintendo"
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
