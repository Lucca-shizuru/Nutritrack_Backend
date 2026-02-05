using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    }
}
