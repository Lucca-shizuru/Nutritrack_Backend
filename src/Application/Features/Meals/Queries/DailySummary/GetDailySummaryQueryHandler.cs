using MediatR;
using Microsoft.EntityFrameworkCore;
using NutriTrack.src.Domain.Core;
using NutriTrack.src.Infraestructure.Persistence;

namespace NutriTrack.src.Application.Features.Meals.Queries.DailySummary
{
    public class GetDailySummaryQueryHandler : IRequestHandler<GetDailySummaryQuery, Result<DailySummaryResponse>>
    {

        private readonly ApplicationDbContext _context;

        public GetDailySummaryQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<DailySummaryResponse>> Handle(GetDailySummaryQuery request, CancellationToken cancellationToken)
        {

            var meals = await _context.Meals
                .Include(m => m.Foods)
                .Where(m => m.UserId == request.UserId && m.Date.Date == request.Date.Date)
                .ToListAsync(cancellationToken);

            if (!meals.Any())
                return Result<DailySummaryResponse>.Success(new DailySummaryResponse(0, 0, 0, 0, 0, 0));

            var response = new DailySummaryResponse(
                TotalCalories: meals.Sum(m => m.TotalCalories),
                TotalProtein: meals.Sum(m => m.TotalProtein),
                TotalCarbs: meals.Sum(m => m.TotalCarbs),
                TotalFat: meals.Sum(m => m.TotalFat),
                TotalQuantity: meals.Sum(m => m.TotalQuantity),
                MealsCount: meals.Count
                );



           return Result<DailySummaryResponse>.Success(response);

        }
    }
}
