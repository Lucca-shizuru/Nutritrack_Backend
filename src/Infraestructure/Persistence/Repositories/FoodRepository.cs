using Microsoft.EntityFrameworkCore;
using NutriTrack.src.Domain.Entities;
using NutriTrack.src.Domain.Interfaces;

namespace NutriTrack.src.Infraestructure.Persistence.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly ApplicationDbContext _context;

        public FoodRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Food?> GetByNameAsync(string name)
        {
          
            return await _context.Foods
                .FirstOrDefaultAsync(f => f.Name.ToLower() == name.ToLower());
        }

        public void Add(Food food)
        {
            _context.Foods.Add(food);
        }
    }
}
