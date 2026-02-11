using MediatR;
using NutriTrack.src.Domain.Core;

namespace NutriTrack.src.Application.Features.Meals.Commands.DeleteMeal
{
    public record DeleteMealCommand(Guid MealId, Guid UserId) : IRequest<Result<Unit>>;
    
    
}
