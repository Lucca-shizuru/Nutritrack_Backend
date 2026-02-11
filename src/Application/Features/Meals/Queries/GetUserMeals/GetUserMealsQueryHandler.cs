using MediatR;
using NutriTrack.src.Domain.Core;
using NutriTrack.src.Domain.Interfaces;
using NutriTrack.src.Infraestructure.ExternalServices.Dtos;

namespace NutriTrack.src.Application.Features.Meals.Queries.GetUserMeals
{
    public class GetUserMealsQueryHandler : IRequestHandler<GetUserMealsQuery, Result<List<MealResponseDto>>>
    {
        private readonly IMealRepository _mealRepository;

        public GetUserMealsQueryHandler(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }
        public async Task<Result<List<MealResponseDto>>> Handle(GetUserMealsQuery request, CancellationToken cancellationToken)
        {
            var meals = await _mealRepository.GetByUserIdAsync(request.UserId);

            
            var dtos = meals.Select(m => new MealResponseDto(
                m.Id,
                "Usuario", 
                "Refeição",
                m.TotalQuantity,
                m.TotalCalories,
                m.TotalProtein,
                m.TotalCarbs,
                m.TotalFat
            )).ToList();

            return Result<List<MealResponseDto>>.Success(dtos);
        }
    }
}
