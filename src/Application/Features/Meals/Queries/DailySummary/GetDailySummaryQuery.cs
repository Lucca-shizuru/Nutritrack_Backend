using MediatR;
using NutriTrack.src.Domain.Core;

namespace NutriTrack.src.Application.Features.Meals.Queries.DailySummary
{
    public record GetDailySummaryQuery(Guid UserId, DateTime Date) : IRequest<Result <DailySummaryResponse>>
    {
    }
}
