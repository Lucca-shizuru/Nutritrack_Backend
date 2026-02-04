using NutriTrack.src.Application.Common.Models;
using NutriTrack.src.Application.Interfaces;
using NutriTrack.src.Domain.Core;
using NutriTrack.src.Infraestructure.ExternalServices.Dtos;


namespace NutriTrack.src.Infraestructure.ExternalServices
{
    public class EdamamNutritionalService : INutritionalDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _appId;
        private readonly string _appKey;

        public string ProviderName => "Edamam";

        public EdamamNutritionalService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _appId = configuration["EdamamApi:AppId"]!;
            _appKey = configuration["EdamamApi:AppKey"]!;
        }

        public async Task<Result<NutritionalInfo>> GetMacrosAsync(string foodName)
        {
            try
            {
                
                var url = $"parser?ingr={Uri.EscapeDataString(foodName)}&app_id={_appId}&app_key={_appKey}";

                var response = await _httpClient.GetFromJsonAsync<EdamamResponse>(url);

                var nutrients = response?.Parsed?.FirstOrDefault()?.Food?.Nutrients;

                if (nutrients == null)
                    return Result<NutritionalInfo>.Failure("Alimento não encontrado na base da Edamam.");

                return Result<NutritionalInfo>.Success(new NutritionalInfo(
                    nutrients.Calories,
                    nutrients.Protein,
                    nutrients.Carbohydrates,
                    nutrients.Fat
                ));
            }
            catch (Exception ex)
            {
                return Result<NutritionalInfo>.Failure($"Erro na comunicação com a API: {ex.Message}");
            }
        }
    }
}
   


