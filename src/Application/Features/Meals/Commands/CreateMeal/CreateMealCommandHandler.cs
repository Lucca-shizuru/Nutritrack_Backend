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
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateMealCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, INutritionalDataService nutritionalDataService)
        {
            _nutritionalDataService = nutritionalDataService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CreateMealCommand request, CancellationToken cancellationToken)
        {
            var nutritionalResult = await _nutritionalDataService.GetMacrosAsync(request.FoodName);

            if (!nutritionalResult.IsSuccess)
                return Result<Guid>.Failure(nutritionalResult.Error!);

           
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                return Result<Guid>.Failure("Usuário não encontrado.");



            var food = new Food
            {
                Id = Guid.NewGuid(),
                Name = request.FoodName
            };

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

            user.AddMeal(meal);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(meal.Id);










        }
    }
}
