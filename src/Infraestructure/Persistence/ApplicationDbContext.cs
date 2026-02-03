using Microsoft.EntityFrameworkCore;
using NutriTrack.src.Domain.Entities;
using NutriTrack.src.Domain.Interfaces;

namespace NutriTrack.src.Infraestructure.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            // Essencial para o PostgreSQL não reclamar das datas em .NET 8/10
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<MealFood> MealsFood { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(u => u.Id);

            modelBuilder.Entity<MealFood>(entity =>
            {
                entity.HasKey(mf => new { mf.MealId, mf.FoodId });

                entity.HasOne(mf => mf.Meal)
                    .WithMany(m => m.MealFoods)
                    .HasForeignKey(mf => mf.MealId);

                entity.HasOne(mf => mf.Food)
                    .WithMany(f => f.MealFoods)
                    .HasForeignKey(mf => mf.FoodId);

                entity.OwnsOne(mf => mf.NutritionalInfo, ni =>
                {
                    ni.Property(p => p.Calories).HasColumnName("Calories").HasPrecision(18, 2);
                    ni.Property(p => p.Protein).HasColumnName("Protein").HasPrecision(18, 2);
                    ni.Property(p => p.Carbs).HasColumnName("Carbs").HasPrecision(18, 2);
                    ni.Property(p => p.Fat).HasColumnName("Fat").HasPrecision(18, 2);
                });
            });
        }
    }
}