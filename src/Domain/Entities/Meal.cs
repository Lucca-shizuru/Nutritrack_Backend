using NutriTrack.src.Domain.Enums;
using NutriTrack.src.Domain.ValueObjects;


namespace NutriTrack.src.Domain.Entities
{
    public class Meal
    {
        public Guid Id { get; private set; }
        public int UserId { get; private set; }
        public DateTime Date { get; private set; }
        public MealType Type { get; private set; }
        private readonly List<MealFood> _mealFoods = new();
        public IReadOnlyCollection<MealFood> MealFoods => _mealFoods.AsReadOnly();

        private Meal() { }

        public Meal(int userId, DateTime date, MealType type)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Date = date;
            Type = type;
        }

        public void AddFood(Food food, decimal quantityInGrams, NutritionalInfo nutritionalInfo)
        {
            if (_mealFoods.Any(mf => mf.FoodId == food.Id))
            {
                throw new InvalidOperationException($"O alimento '{food.Name}' já existe nesta refeição.");
            }

            var mealFood = new MealFood(this.Id, food.Id, quantityInGrams, nutritionalInfo);
            _mealFoods.Add(mealFood);
        }

        public void RemoveFood(Guid foodId)
        {
            var mealFoodToRemove = _mealFoods.FirstOrDefault(mf => mf.FoodId == foodId);
            if (mealFoodToRemove is null)
            {
                throw new InvalidOperationException("O alimento não foi encontrado nesta refeição.");
            }
            _mealFoods.Remove(mealFoodToRemove);
        }

        public NutritionalInfo GetTotalNutritionalInfo()
        {
            return _mealFoods
                .Select(mf => mf.NutritionalInfo)
                .Aggregate(NutritionalInfo.Zero, (current, next) => current + next);
        }
    }
}