using NutriTrack.src.Domain.Enums;


namespace NutriTrack.src.Domain.Entities
{
    public class MealRequest
    {
        public Guid? UserId { get; set; }
        public DateTime Date { get; set; }
        public MealType Type { get; set; }

        public List<FoodRequest> Food { get; set; }
    }
}
