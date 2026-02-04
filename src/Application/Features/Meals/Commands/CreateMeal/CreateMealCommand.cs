using MediatR;
using NutriTrack.src.Domain.Core;
using NutriTrack.src.Domain.Enums;
using NutriTrack.src.Infraestructure.ExternalServices.Dtos;

namespace NutriTrack.src.Application.Features.Users.Commands.CreateMeal
{
    

    public record CreateMealCommand : IRequest<Result <MealResponseDto>>
    {
        public Guid UserId { get; init; }
        public required string FoodName { get; init; }
        public decimal Quantity { get; init; }
        public DateTime Date { get; init; }
        public MealType Type { get; init; }
    }
}

