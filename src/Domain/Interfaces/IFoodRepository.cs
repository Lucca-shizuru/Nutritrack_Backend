using NutriTrack.src.Domain.Entities;

namespace NutriTrack.src.Domain.Interfaces
{
    public interface IFoodRepository
    {
        Task<Food?> GetByNameAsync(string name);
        void Add(Food food);

    }
}
