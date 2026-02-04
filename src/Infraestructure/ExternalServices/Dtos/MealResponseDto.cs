namespace NutriTrack.src.Infraestructure.ExternalServices.Dtos
{
    public record MealResponseDto(
     Guid Id,
     string FoodName,
     decimal Quantity,
     decimal Calories,
     decimal Protein,
     decimal Carbs,
     decimal Fat
 );
}
