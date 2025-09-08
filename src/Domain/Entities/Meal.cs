using NutriTrack.src.Domain.Enums;


namespace NutriTrack.src.Domain.Entities
{
    public class Meal
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public DateTime Date { get; set; }
        public MealType Type { get; set; }
        public required User User { get; set; }
        public ICollection<MealFood> MealFoods { get; set; }
    }
}
