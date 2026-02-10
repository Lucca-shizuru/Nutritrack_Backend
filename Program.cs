using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NutriTrack.src.Application.Common.Events;
using NutriTrack.src.Application.Features.Meals.Commands.CreateMeal;
using NutriTrack.src.Application.Features.Users.Commands.CreateMeal;
using NutriTrack.src.Application.Interfaces;
using NutriTrack.src.Domain.Interfaces;
using NutriTrack.src.Infraestructure.ExternalServices;
using NutriTrack.src.Infraestructure.Persistence;
using NutriTrack.src.Infraestructure.Persistence.Repositories;
using NutriTrack.src.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


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
                 options.UseNpgsql(
                     builder.Configuration.GetConnectionString("DefaultConnection"),
                     b => b.MigrationsAssembly("NutriTrack") // Diz à API onde as migrations estão
             ));

            builder.Services.AddHttpClient<INutritionalDataService, EdamamNutritionalService>(client =>
            {
                client.BaseAddress = new Uri("https://api.edamam.com/api/food-database/v2/");
            });

            builder.Services.AddScoped<IMealRepository, MealRepository>();

            builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            builder.Services.AddScoped<IFoodRepository, FoodRepository>();

            builder.Services.AddMediatR(cfg =>
                 cfg.RegisterServicesFromAssembly(typeof(CreateMealCommandHandler).Assembly));

            builder.Services.AddSingleton<IPasswordHashingService, PasswordHashingService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddMassTransit(x =>
            {
              
                x.AddConsumer<MealCreatedConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            // 2. Configuração da Autenticação JWT
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");

            builder.Services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings["Secret"]!))
                });




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
