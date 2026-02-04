using MediatR;
using Microsoft.AspNetCore.Mvc;
using NutriTrack.src.Application.Features.Users.Commands.CreateMeal;

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

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
               
                return BadRequest(new { message = result.Error });
            }

           
            return Created(string.Empty, new { id = result.Value });
        }
    }
}
