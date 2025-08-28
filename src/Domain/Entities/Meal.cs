using NutriTrack.src.Domain.Enums;


namespace NutriTrack.src.Domain.Entities
{
    public class Meal
    {
        public Guid? UserId { get; set; }
        public DateTime Date { get; set; }
        public MealType Type { get; set; }
        public User User { get; set; }
        public ICollection<MealFood> MealFoods { get; set; }
    }
}
