using MediatR;
using NutriTrack.src.Application.Common.Models;
using NutriTrack.src.Domain.Core;
using NutriTrack.src.Infraestructure.ExternalServices.Dtos;

namespace NutriTrack.src.Application.Features.Meals.Commands.UpdateMeal
{
    public class UpdateMealCommand : IRequest<Result<MealResponseDto>>
    {
        public Guid MealId { get; set; }
        public Guid UserId { get; set; } 
        public List<MealFoodRequest> Foods { get; set; } = new();
        public DateTime Date { get; set; }
        public int Type { get; set; }
    }
}
