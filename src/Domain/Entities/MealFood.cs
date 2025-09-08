namespace NutriTrack.src.Domain.Entities
{
    public class MealFood
    {
        public Guid MealId { get; set; }
        public Guid FoodId { get; set; }
        public decimal QuantityInGrams { get; set; }

        public decimal Calories { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbs { get; set; }
        public decimal Fat { get; set; }

        public required Meal Meal { get; set; }
        public required Food Food { get; set; }
    }
}
