using MediatR;
using NutriTrack.src.Domain.Entities;
using NutriTrack.src.Domain.Interfaces;

namespace NutriTrack.src.Application.Commands.CreateMeal
{
    public class CreateMealCommandHandler : IRequestHandler<CreateMealCommand, Guid>
    {
        private readonly IMealRepository _mealRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateMealCommandHandler(IMealRepository mealRepository, IUnitOfWork unitOfWork)
        {
            _mealRepository = mealRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(CreateMealCommand request, CancellationToken cancellationToken)
        {

            var meal = new Meal(
                request.UserId,
                request.Date,
                request.Type
            );


            _mealRepository.Add(meal);


            await _unitOfWork.SaveChangesAsync();

            return meal.Id;

        }
    }
}
