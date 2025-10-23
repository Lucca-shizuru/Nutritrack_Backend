 using NutriTrack.src.Domain.ValueObjects;

namespace NutriTrack.src.Domain.Entities
{
    public class MealFood
    {
        public Guid MealId { get; private set; } 
        public Guid FoodId { get; private set; } 
        public decimal QuantityInGrams { get; private set; }
        public NutritionalInfo NutritionalInfo { get; private set; } = null!; 


        public Meal Meal { get; private set; } = null!;
        public Food Food { get; private set; } = null!;

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