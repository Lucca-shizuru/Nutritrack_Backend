using NutriTrack.src.Application.Common.Models;
using NutriTrack.src.Domain.Core;


namespace NutriTrack.src.Application.Interfaces
{
    public interface INutritionalDataService
    {
        string ProviderName { get; }

        Task<Result<NutritionalInfo>> GetMacrosAsync(string foodName);
    }
}
