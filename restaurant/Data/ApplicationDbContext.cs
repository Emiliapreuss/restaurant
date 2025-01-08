using restaurant.Models;
using Microsoft.EntityFrameworkCore;

namespace restaurant.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Menu> Menus { get; set; }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>()
               .HasMany(m => m.Dishes)
               .WithOne(d => d.Menu)
               .HasForeignKey(d => d.MenuId);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Dishes)
                .WithOne(d => d.Category)
                .HasForeignKey(d => d.CategoryId);

            modelBuilder.Entity<Dish>()
                .Property(d => d.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Event>()
               .HasOne(e => e.Menu)
               .WithMany(m => m.Events)
               .HasForeignKey(e => e.MenuId);


        }
    }
}
