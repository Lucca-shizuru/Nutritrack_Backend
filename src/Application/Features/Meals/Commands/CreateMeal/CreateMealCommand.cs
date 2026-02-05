using MediatR;
using NutriTrack.src.Domain.Core;
using NutriTrack.src.Domain.Enums;
using NutriTrack.src.Infraestructure.ExternalServices.Dtos;

namespace NutriTrack.src.Application.Features.Users.Commands.CreateMeal
{
    

    public class CreateMealCommand : IRequest<Result <MealResponseDto>>
    {
        public Guid UserId { get; init; }
        public required List<MealFoodRequest> Foods { get; init; }
        public decimal Quantity { get; init; }
        public DateTime Date { get; init; }
        public MealType Type { get; init; }
    }
}

