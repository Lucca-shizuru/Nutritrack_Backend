using MediatR;
using Microsoft.AspNetCore.Mvc;
using NutriTrack.src.Application.Features.Users.Commands.RegisterUser;
using NutriTrack.src.Domain.Core;

namespace NutriTrack.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            {
                var result = await _mediator.Send(command);

                if (!result.IsSuccess)
                {

                    return BadRequest(new { error = result.Error });
                }


                return CreatedAtAction(nameof(Register), new { id = result.Value }, result.Value);
            }
        }
    }
}
    
