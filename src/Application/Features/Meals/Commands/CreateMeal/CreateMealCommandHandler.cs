    using MassTransit;
    using MediatR;
    using NutriTrack.src.Application.Common.Events;
    using NutriTrack.src.Application.Common.Models;
    using NutriTrack.src.Application.Features.Users.Commands.CreateMeal;
    using NutriTrack.src.Application.Interfaces;
    using NutriTrack.src.Domain.Core;
    using NutriTrack.src.Domain.Entities;
    using NutriTrack.src.Domain.Interfaces;
    using NutriTrack.src.Infraestructure.ExternalServices.Dtos;
    using DomainVO = NutriTrack.src.Domain.ValueObjects;




namespace NutriTrack.src.Application.Features.Meals.Commands.CreateMeal
{
    public class CreateMealCommandHandler : IRequestHandler<CreateMealCommand, Result<MealResponseDto>>
    {
        private readonly INutritionalDataService _nutritionalDataService;
        private readonly IFoodRepository _foodRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMealRepository _mealRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ITranslationService _translationService;
        public CreateMealCommandHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            INutritionalDataService nutritionalDataService,
            IFoodRepository foodRepository,
            IMealRepository mealRepository,
            IPublishEndpoint publishEndpoint,
            ITranslationService translationService
            )
        {
            _nutritionalDataService = nutritionalDataService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _foodRepository = foodRepository;
            _mealRepository = mealRepository;
            _publishEndpoint = publishEndpoint;
            _translationService = translationService;
        }
        public async Task<Result<MealResponseDto>> Handle(CreateMealCommand request, CancellationToken cancellationToken)
        {



            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                return Result<MealResponseDto>.Failure("Usuário não encontrado.");

            var meal = new Meal(request.UserId, request.Date, request.Type);

            foreach (var item in request.Foods)
            {
                var food = await _foodRepository.GetByNameAsync(item.FoodName);
                DomainVO.NutritionalInfo baseMacros;

                if (food != null)
                {

                    Console.WriteLine($"[DB CACHE] Alimento '{item.FoodName}' encontrado no banco.");
                    baseMacros = food.BaseNutritionalInfo;
                }
                else
                {

                    Console.WriteLine($"[API] Buscando '{item.FoodName}' nas APIs externas...");

                    var englishName = await _translationService.TranslateToEnglishAsync(item.FoodName);
                    Console.WriteLine($"[DEBUG] Traduzindo '{item.FoodName}' -> '{englishName}'");

                    var nutritionalResult = await _nutritionalDataService.GetMacrosAsync(englishName);


                    if (!nutritionalResult.IsSuccess || nutritionalResult.Value is null)
                    {
                        Console.WriteLine($"[ERRO] Edamam não encontrou: {englishName}");
                        continue;
                    }

                    var apiMacros = nutritionalResult.Value;


                    baseMacros = new DomainVO.NutritionalInfo(
                        (decimal)apiMacros.Calories,
                        (decimal)apiMacros.Protein,
                        (decimal)apiMacros.Carbohydrates,
                        (decimal)apiMacros.Fat
                        );




                    food = new Food(item.FoodName, baseMacros);


                    _foodRepository.Add(food);
                }

                decimal factor = item.Quantity / 100m;

                var adjustedMacros = new DomainVO.NutritionalInfo(
                    baseMacros.Calories * factor,
                    baseMacros.Protein * factor,
                    baseMacros.Carbs * factor,
                    baseMacros.Fat * factor
                );


                meal.AddFood(food.Id, item.FoodName, item.Quantity, adjustedMacros);
            }


            _mealRepository.Add(meal);
            await _unitOfWork.SaveChangesAsync(cancellationToken);


            await _publishEndpoint.Publish(new MealCreatedEvent(
                meal.Id,
                user.Id,
                $"Refeição Completa ({request.Foods.Count} itens)",
                meal.TotalCalories
            ), cancellationToken);


            var response = new MealResponseDto(
                meal.Id,                        
                user.Name,                      
                "Refeição Completa",          
                meal.TotalQuantity,             
                meal.TotalCalories,             
                meal.TotalProtein,              
                meal.TotalCarbs,                
                meal.TotalFat

            );

            return Result<MealResponseDto>.Success(response);
        }






    }
}

        
