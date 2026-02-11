using NutriTrack.src.Domain.Enums;
using NutriTrack.src.Domain.ValueObjects;


namespace NutriTrack.src.Domain.Entities
{
    public class Meal
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime Date { get; private set; }
        public MealType Type { get; private set; }



        private readonly List<MealFood> _foods = new();
        public virtual ICollection<MealFood> Foods => _foods;

        public decimal TotalCalories => _foods.Sum(f => f.NutritionalInfo.Calories);
        public decimal TotalProtein => _foods.Sum(f => f.NutritionalInfo.Protein);
        public decimal TotalCarbs => _foods.Sum(f => f.NutritionalInfo.Carbs);
        public decimal TotalFat => _foods.Sum(f => f.NutritionalInfo.Fat);
        public decimal TotalQuantity => _foods.Sum(f => f.QuantityInGrams);


        private Meal() { }

        public Meal(Guid userId, DateTime date, MealType type)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Date = date;
            Type = type;
        }

        public void AddFood(Guid foodId, string foodName, decimal quantityInGrams, NutritionalInfo nutritionalInfo)
        {
            if (Foods.Any(mf => mf.FoodId == foodId))
            {
                throw new InvalidOperationException($"O alimento '{foodName}' já existe nesta refeição.");
            }

            var mealFood = new MealFood(this.Id, foodId, quantityInGrams, nutritionalInfo);
            Foods.Add(mealFood);
        }

        public void ClearFoods()
        {
            _foods.Clear();
        }

        
        public void UpdateDateAndType(DateTime date, MealType type)
        {
            Date = date;
            Type = type;
        }

        public NutritionalInfo GetTotalNutritionalInfo()
        {
            if (!Foods.Any()) return NutritionalInfo.Zero;

            return _foods
                .Select(mf => mf.NutritionalInfo)
                .Aggregate(NutritionalInfo.Zero, (current, next) => current + next);
        }

    }
}