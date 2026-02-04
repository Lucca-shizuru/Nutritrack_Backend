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
        

        private Meal() { }

        public virtual ICollection<MealFood> Foods { get; private set; } = new List<MealFood>();

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

        public NutritionalInfo GetTotalNutritionalInfo()
        {
            if (!Foods.Any()) return NutritionalInfo.Zero;

            return Foods
                .Select(mf => mf.NutritionalInfo)
                .Aggregate(NutritionalInfo.Zero, (current, next) => current + next);
        }
    }
}