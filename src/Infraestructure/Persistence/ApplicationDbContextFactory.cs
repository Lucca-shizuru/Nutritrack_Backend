using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace NutriTrack.src.Infraestructure.Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var connectionString = "Host=localhost;Port=5432;Database=NutriTrack;Username=postgres;Password=123";

           
            optionsBuilder.UseNpgsql(connectionString, b =>
            {
       
                b.MigrationsAssembly("NutriTrack");
            });

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
