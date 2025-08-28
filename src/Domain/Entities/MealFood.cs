namespace NutriTrack.src.Domain.Entities
{
    public class MealFood
    {
        public Guid MealId { get; set; }
        public Guid FoodId { get; set; }
        public decimal QuantityInGrams { get; set; }

        public Meal Meal { get; set; }
        public Food Food { get; set; }
    }
}
