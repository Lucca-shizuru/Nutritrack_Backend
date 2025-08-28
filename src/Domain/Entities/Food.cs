namespace NutriTrack.src.Domain.Entities
{
    public class Food
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<MealFood> MealFoods { get; set; }
    }
}
