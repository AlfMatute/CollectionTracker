using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CollectionTrackerAPI.Models
{
    public class ApplicationDbContext : IdentityDbContext<CollectionUser>
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Condition> Conditions { get; set; }

        public DbSet<Collection> Collections { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Collection>()
            //    .HasKey(fk => new { fk.Brand, fk.Condition, fk.Category });
        }

    }
}
