using Microsoft.EntityFrameworkCore;
using Veterinary.web.Models;

namespace Veterinary.web.Data
{
          public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
            { }
            public DbSet<Veterinarian> Veterinarians { get; set; }
            public DbSet<Owner> Owners { get; set; }
            public DbSet<Pet> Pets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Owner>().HasIndex(t => t.Id).IsUnique();
                modelBuilder.Entity<Pet>().HasIndex(t => t.Id).IsUnique();
                modelBuilder.Entity<Veterinarian>().HasIndex(t => t.Id).IsUnique();
        }

    }
}
    
