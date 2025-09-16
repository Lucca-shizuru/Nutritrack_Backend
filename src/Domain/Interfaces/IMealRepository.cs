using NutriTrack.src.Domain.Entities;

namespace NutriTrack.src.Domain.Interfaces
{
    public interface IMealRepository
    {
        Task<Meal?> GetByIdAsync(Guid id);
        Task<IEnumerable<Meal>> GetAllAsync();
        void Add(Meal meal);
        void Update(Meal meal);
        void Delete(Meal meal);
    }
}
