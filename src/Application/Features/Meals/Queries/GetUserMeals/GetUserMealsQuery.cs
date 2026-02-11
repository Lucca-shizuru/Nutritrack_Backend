using MediatR;
using NutriTrack.src.Domain.Core;
using NutriTrack.src.Infraestructure.ExternalServices.Dtos;

namespace NutriTrack.src.Application.Features.Meals.Queries.GetUserMeals
{
    public record GetUserMealsQuery(Guid UserId) : IRequest<Result<List<MealResponseDto>>>;
    
   
}
