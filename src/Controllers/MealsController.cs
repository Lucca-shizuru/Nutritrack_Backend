using MediatR;
using Microsoft.AspNetCore.Mvc;
using NutriTrack.src.Application.Commands.CreateMeal;

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
        public async Task<IActionResult> CreateMeal([FromBody] CreateMealCommand command)
        {

            var newMealId = await _mediator.Send(command);
 
            return Ok(new { id = newMealId });
        }
    }
}
