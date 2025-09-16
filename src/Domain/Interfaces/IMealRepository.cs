using NutriTrack.src.Domain.Entities;

namespace NutriTrack.src.Domain.Interfaces
{
    public interface IMealRepository
    {
        Task<Meal?> GetByIdAsync(Guid id);
        Task<IEnumerable<Meal>> GetAllAsync();
        Task AddAsync(Meal meal);
        Task UpdateAsync(Meal meal);
        Task DeleteAsync(int id);
    }
}
