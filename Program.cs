using MediatR
using Microsoft.EntityFrameworkCore;
using NutriTrack.src.Application.Commands.CreateMeal;
using NutriTrack.src.Domain.Interfaces;
using NutriTrack.src.Infraestructure.Persistence;
using NutriTrack.src.Infraestructure.Persistence.Repositories;

namespace NutriTrack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.




            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IMealRepository, MealRepository>();

            builder.Services.AddScoped<IUnitOfWork, ApplicationDbContext>();

            object value = builder.Services.AddMediatR(cfg =>
                 cfg.RegisterServicesFromAssembly(typeof(CreateMealCommandHandler).Assembly));

            builder.Services.AddSingleton<IPasswordHashingService, BCryptPasswordHashingService>();




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
