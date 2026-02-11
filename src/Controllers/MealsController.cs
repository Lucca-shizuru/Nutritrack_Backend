using MediatR;
using Microsoft.AspNetCore.Mvc;
using NutriTrack.src.Application.Features.Meals.Commands.DeleteMeal;
using NutriTrack.src.Application.Features.Meals.Commands.UpdateMeal;
using NutriTrack.src.Application.Features.Meals.Queries.DailySummary;
using NutriTrack.src.Application.Features.Users.Commands.CreateMeal;
using NutriTrack.src.Infraestructure.ExternalServices.Dtos;

namespace NutriTrack.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealsController : Controller
    {
        private readonly IMediator _mediator;

        public MealsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost] 
        public async Task<ActionResult<MealResponseDto>> CreateMeal([FromBody] CreateMealCommand command)
        {   

            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

        [HttpGet("daily-summary")]
        public async Task<ActionResult<DailySummaryResponse>> GetDailySummary([FromQuery] Guid userId, [FromQuery] DateTime date)
        {
            var query = new GetDailySummaryQuery(userId, date);
            var result = await _mediator.Send(query);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeal(Guid id, [FromBody] UpdateMealCommand command)
        {
            if (id != command.MealId)
                return BadRequest("ID da URL difere do ID do corpo.");

            
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) return Unauthorized();

            command.UserId = Guid.Parse(userIdClaim);

            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal(Guid id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) return Unauthorized();

            var command = new DeleteMealCommand(id, Guid.Parse(userIdClaim));

            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result.Error);

            return NoContent(); 
        }


    }
}
