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
                .Include(m => m.Foods)
                    .ThenInclude(mf => mf.Food) 
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Meal>> GetAllAsync()
        {
            return await _context.Meals
                .Include(m => m.Foods)
                    .ThenInclude(mf => mf.Food)
                .ToListAsync();
        }
        public async Task<IEnumerable<Meal>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Meals
                .Include(m => m.Foods) 
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.Date) 
                .ToListAsync();
        }

        public void Add(Meal meal)
        {
             _context.Meals.Add(meal);
        }

        public void Update(Meal meal)
        {
            _context.Meals.Update(meal);
           
        }

        public void Delete(Meal meal)
        {
                _context.Meals.Remove(meal);
               
          
        }


    }
}
