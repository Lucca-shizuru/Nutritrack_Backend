namespace NutriTrack.src.Application.Common.Events
{
    public record MealCreatedEvent(
        Guid MealId,
        Guid UserId,
        string FoodName,
        decimal Calories
    );
    

   
}
