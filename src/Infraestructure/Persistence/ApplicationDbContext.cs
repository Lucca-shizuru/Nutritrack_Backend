using Microsoft.EntityFrameworkCore;
using NutriTrack.src.Domain.Entities;
using NutriTrack.src.Domain.Interfaces;

namespace NutriTrack.src.Infraestructure.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
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

            modelBuilder.Entity<MealFood>().OwnsOne(mf => mf.NutritionalInfo, navBuilder =>
            {
                navBuilder.Property(ni => ni.Calories).HasColumnName("Calories");
                navBuilder.Property(ni => ni.Protein).HasColumnName("Protein");
                navBuilder.Property(ni => ni.Carbs).HasColumnName("Carbs");
                navBuilder.Property(ni => ni.Fat).HasColumnName("Fat");
            });

            modelBuilder.Entity<User>(entity =>
            {
            
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();
            });

        }
    }
}
