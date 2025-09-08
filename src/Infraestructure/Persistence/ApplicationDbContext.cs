using Microsoft.EntityFrameworkCore;
using NutriTrack.src.Domain.Entities;

namespace NutriTrack.src.Infraestructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {

    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }
    
    public DbSet<User> Users { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<MealFood> MealsFood { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MealFood>()
                .HasKey(mf => new { mf.MealId, mf.FoodId });
        }
    }


}
