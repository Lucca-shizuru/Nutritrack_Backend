using NutriTrack.src.Domain.Entities;

namespace NutriTrack.src.Domain.Interfaces
{
    public interface InutritionApiService
    {
        Task<NutritionResponse> GetNutritionResponse(string foodName, decimal grams);
    }
}
