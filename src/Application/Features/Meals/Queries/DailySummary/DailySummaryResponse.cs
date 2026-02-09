namespace NutriTrack.src.Application.Features.Meals.Queries.DailySummary
{
    public record DailySummaryResponse(
        decimal TotalCalories,
        decimal TotalProtein,
        decimal TotalCarbs,
        decimal TotalFat,
        decimal TotalQuantity,
        int MealsCount
    );
    
   
}
