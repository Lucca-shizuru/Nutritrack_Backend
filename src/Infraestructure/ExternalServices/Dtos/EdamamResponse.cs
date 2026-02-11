using System.Text.Json.Serialization;

namespace NutriTrack.src.Infraestructure.ExternalServices.Dtos
{
    public class EdamamResponse
    {
        [JsonPropertyName("parsed")]
        public List<ParsedFoodDto> Parsed { get; set; } = new();

        [JsonPropertyName("hints")]
        public List<HintItem>? Hints { get; set; }
    }

    public class ParsedFoodDto
    {
        [JsonPropertyName("food")]
        public FoodDto Food { get; set; } = null!;
    }

    public class HintItem
    {
        public FoodDto? Food { get; set; }
    }

    public class FoodDto
    {
        [JsonPropertyName("nutrients")]
        public NutrientsDto Nutrients { get; set; } = null!;
    }

    public class NutrientsDto
    {
        [JsonPropertyName("ENERC_KCAL")] public double Calories { get; set; }
        [JsonPropertyName("PROCNT")] public double Protein { get; set; }
        [JsonPropertyName("FAT")] public double Fat { get; set; }
        [JsonPropertyName("CHOCDF")] public double Carbohydrates { get; set; }
    }
}