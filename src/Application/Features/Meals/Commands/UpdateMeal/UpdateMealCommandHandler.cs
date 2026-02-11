using MediatR;

using NutriTrack.src.Application.Interfaces;
using NutriTrack.src.Domain.Core;
using NutriTrack.src.Domain.Entities;
using NutriTrack.src.Domain.Interfaces;
using NutriTrack.src.Infraestructure.ExternalServices.Dtos;
using DomainVO = NutriTrack.src.Domain.ValueObjects;

namespace NutriTrack.src.Application.Features.Meals.Commands.UpdateMeal
{
    public class UpdateMealCommandHandler : IRequestHandler<UpdateMealCommand, Result<MealResponseDto>>
    {
        private readonly IMealRepository _mealRepository;
        private readonly IFoodRepository _foodRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INutritionalDataService _nutritionalService;
        private readonly ITranslationService _translationService;

        public UpdateMealCommandHandler(
            IMealRepository mealRepository,
            IFoodRepository foodRepository,
            IUnitOfWork unitOfWork,
            INutritionalDataService nutritionalService,
            ITranslationService translationService)
        {
            _mealRepository = mealRepository;
            _foodRepository = foodRepository;
            _unitOfWork = unitOfWork;
            _nutritionalService = nutritionalService;
            _translationService = translationService;
        }

        public async Task<Result<MealResponseDto>> Handle(UpdateMealCommand request, CancellationToken cancellationToken)
        {
            
            var meal = await _mealRepository.GetByIdAsync(request.MealId);

            if (meal == null )
                return Result<MealResponseDto>.Failure("Refeição não encontrada.");

            if (meal.UserId != request.UserId)
                return Result<MealResponseDto>.Failure("A refeição pertence a outro usuário.");


            meal.ClearFoods();

            meal.UpdateDateAndType(request.Date, (NutriTrack.src.Domain.Enums.MealType)request.Type);

            foreach (var item in request.Foods)
            {
                var englishName = await _translationService.TranslateToEnglishAsync(item.FoodName);
                var nutritionalResult = await _nutritionalService.GetMacrosAsync(englishName);

                if (!nutritionalResult.IsSuccess || nutritionalResult.Value is null) continue;

                var baseMacros = nutritionalResult.Value;

                
                var food = await _foodRepository.GetByNameAsync(item.FoodName);
                if (food == null)
                {
                    food = new Food { Id = Guid.NewGuid(), Name = item.FoodName };
                    _foodRepository.Add(food);
                }

                
                var factor = (double)item.Quantity / 100.0;
                var adjustedMacros = new DomainVO.NutritionalInfo(
                    (decimal)(baseMacros.Calories * (double)factor),
                    (decimal)(baseMacros.Protein * (double)factor),
                    (decimal)(baseMacros.Carbohydrates * (double)factor),
                    (decimal)(baseMacros.Fat * (double)factor)
                );

                meal.AddFood(food.Id, item.FoodName, item.Quantity, adjustedMacros);
            }

           
            _mealRepository.Update(meal);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

           
            return Result<MealResponseDto>.Success(new MealResponseDto(
                meal.Id, "Usuário", "Refeição Atualizada",
                meal.TotalQuantity, meal.TotalCalories, meal.TotalProtein, meal.TotalCarbs, meal.TotalFat
            ));
        }
    }
}