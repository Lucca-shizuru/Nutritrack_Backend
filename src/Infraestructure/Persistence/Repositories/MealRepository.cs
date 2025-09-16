using Microsoft.EntityFrameworkCore;
using NutriTrack.src.Domain.Entities;
using NutriTrack.src.Domain.Interfaces;

namespace NutriTrack.src.Infraestructure.Persistence.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly ApplicationDbContext _context;

        public MealRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Meal?> GetByIdAsync(Guid id)
        {
            return await _context.Meals
                .Include(m => m.MealFoods)
                    .ThenInclude(mf => mf.Food) 
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Meal>> GetAllAsync()
        {
            return await _context.Meals
                .Include(m => m.MealFoods)
                    .ThenInclude(mf => mf.Food)
                .ToListAsync();
        }

        public async Task Add(Meal meal)
        {
            await _context.Meals.Add(meal);
        }

        public async Task Update(Meal meal)
        {
            _context.Meals.Update(meal);
           
        }

        public async Task Delete(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            if (meal != null)
            {
                _context.Meals.Remove(meal);
               
            }
        }


    }
}
