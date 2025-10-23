using MediatR;
using NutriTrack.src.Domain.Enums;

namespace NutriTrack.src.Application.Commands.CreateMeal
{
    

    public record CreateMealCommand : IRequest<Guid>
    {
        public Guid UserId { get; init; }
        public DateTime Date { get; init; }
        public MealType Type { get; init; }
    }
}

