using NutriTrack.src.Application.Common.Models;
using NutriTrack.src.Application.Interfaces;
using NutriTrack.src.Domain.Core;
using NutriTrack.src.Infraestructure.ExternalServices.Dtos;
using System.Net.Http.Json;


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
            _appId = configuration["EdamamApi:AppId"] ?? throw new ArgumentNullException("EdamamApi:AppId não configurado");
            _appKey = configuration["EdamamApi:AppKey"] ?? throw new ArgumentNullException("EdamamApi:AppKey não configurado");
        }

        public async Task<Result<NutritionalInfo>> GetMacrosAsync(string foodName)
        {
            try
            {
                
                var url = $"parser?ingr={Uri.EscapeDataString(foodName)}&app_id={_appId}&app_key={_appKey}";

                var response = await _httpClient.GetFromJsonAsync<EdamamResponse>(url);

            

                if (response == null)
                    return Result<NutritionalInfo>.Failure("Alimento não encontrado na base da Edamam.");

                FoodDto? foodData = response.Parsed?.FirstOrDefault()?.Food;

             
                if (foodData == null)
                {
                    foodData = response.Hints?.FirstOrDefault()?.Food;
                }

               
                if (foodData?.Nutrients == null)
                    return Result<NutritionalInfo>.Failure($"Alimento '{foodName}' não encontrado (nem em Parsed, nem em Hints).");

                var nutrients = foodData.Nutrients;

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
   


