 using NutriTrack.src.Domain.ValueObjects;

namespace NutriTrack.src.Domain.Entities
{
    public class MealFood
    {
        public Guid MealId { get; set; } 
        public Guid FoodId { get;  set; } 
        public decimal QuantityInGrams { get; set; }
        public NutritionalInfo NutritionalInfo { get; set; } = null!; 


        public Meal Meal { get; set; } = null!;
        public Food Food { get; set; } = null!;

        private MealFood() { }

        internal MealFood(Guid mealId, Guid foodId, decimal quantityInGrams, NutritionalInfo nutritionalInfo)
        {
            if (quantityInGrams <= 0)
            {
                throw new ArgumentException("A quantidade em gramas deve ser positiva.", nameof(quantityInGrams));
            }

            MealId = mealId;
            FoodId = foodId;
            QuantityInGrams = quantityInGrams;
            NutritionalInfo = nutritionalInfo;
        }
    }
}