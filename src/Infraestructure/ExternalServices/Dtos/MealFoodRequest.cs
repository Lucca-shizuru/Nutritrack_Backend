namespace NutriTrack.src.Infraestructure.ExternalServices.Dtos
{
    public class MealFoodRequest
    {
        public required string FoodName { get; init; }
        public decimal Quantity { get; init; }
    }
}
