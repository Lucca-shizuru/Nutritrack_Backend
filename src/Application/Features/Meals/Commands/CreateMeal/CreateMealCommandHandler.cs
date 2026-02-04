using MediatR;
using NutriTrack.src.Application.Common.Models;
using NutriTrack.src.Application.Features.Users.Commands.CreateMeal;
using NutriTrack.src.Application.Interfaces;
using NutriTrack.src.Domain.Core;
using NutriTrack.src.Domain.Entities;
using NutriTrack.src.Domain.Interfaces;
using DomainVO = NutriTrack.src.Domain.ValueObjects;

namespace NutriTrack.src.Application.Features.Meals.Commands.CreateMeal
{
    public class CreateMealCommandHandler : IRequestHandler<CreateMealCommand, Result <Guid>>
    {
        private readonly INutritionalDataService _nutritionalDataService;
        private readonly IFoodRepository _foodRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMealRepository _mealRepository;
        public CreateMealCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, INutritionalDataService nutritionalDataService, IFoodRepository foodRepository, IMealRepository mealRepository)
        {
            _nutritionalDataService = nutritionalDataService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _foodRepository = foodRepository;
            _mealRepository = mealRepository;
        }
        public async Task<Result<Guid>> Handle(CreateMealCommand request, CancellationToken cancellationToken)
        {
            var nutritionalResult = await _nutritionalDataService.GetMacrosAsync(request.FoodName);

            if (!nutritionalResult.IsSuccess)
                return Result<Guid>.Failure(nutritionalResult.Error!);

           
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                return Result<Guid>.Failure("Usuário não encontrado.");



            var food = await _foodRepository.GetByNameAsync(request.FoodName);
            if (food == null)
            {
                food = new Food { Id = Guid.NewGuid(), Name = request.FoodName };
                _foodRepository.Add(food);
            }

            var baseMacros = nutritionalResult.Value!;
            var factor = (double)request.Quantity / 100.0;

            var adjustedMacros = new DomainVO.NutritionalInfo(
                (decimal)(baseMacros.Calories * (double)factor),
                (decimal)(baseMacros.Protein * (double)factor),
                (decimal)(baseMacros.Carbohydrates * (double)factor),
                (decimal)(baseMacros.Fat * (double)factor)
            );

            var meal = new Meal(request.UserId,request.Date, request.Type);

            meal.AddFood(food.Id, request.FoodName, request.Quantity, adjustedMacros);

            _mealRepository.Add(meal);


            user.AddMeal(meal);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(meal.Id);










        }
    }
}
