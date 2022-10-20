using Database.Models;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace Database
{
    public class BurgerBackendDbContext : DbContext
    {
        public BurgerBackendDbContext(DbContextOptions<BurgerBackendDbContext> options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; } = null!;
        public DbSet<Rating> Ratings { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .HasMany(x => x.Ratings)
                .WithOne(x => x.Restaurant)
                .HasForeignKey(x => x.RestaurantId);

            modelBuilder.Entity<Rating>();
            modelBuilder.Entity<User>()
                .HasMany(x => x.MyRatings)
                .WithOne(x => x.RatedByUser)
                .HasForeignKey(x => x.RatedByUserId);


            base.OnModelCreating(modelBuilder);
        }
    }
}