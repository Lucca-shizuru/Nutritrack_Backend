using MediatR;
using NutriTrack.src.Domain.Core;
using NutriTrack.src.Domain.Interfaces;

namespace NutriTrack.src.Application.Features.Meals.Commands.DeleteMeal
{
    public class DeleteMealCommandHandler : IRequestHandler<DeleteMealCommand, Result<Unit>>
    {

        private readonly IMealRepository _mealRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMealCommandHandler(IMealRepository mealRepository, IUnitOfWork unitOfWork)
        {
            _mealRepository = mealRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Unit>> Handle(DeleteMealCommand request, CancellationToken cancellationToken)
        {
            var meal = await _mealRepository.GetByIdAsync(request.MealId);

            if (meal == null)
            {
                return Result<Unit>.Failure("refeição não encontrada ");
            }
            if (meal.UserId != request.UserId)
            {
                return Result<Unit>.Failure("A refeição não pertence ao usuário especificado.");
            }
            _mealRepository.Delete(meal);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
