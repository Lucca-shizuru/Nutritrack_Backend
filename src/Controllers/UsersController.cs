using MediatR;
using Microsoft.AspNetCore.Mvc;
using NutriTrack.src.Application.Features.Users.Commands.RegisterUser;

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
            try
            {
                var userId = await _mediator.Send(command);
                return Ok(new { Message = "Usuário registrado com sucesso!", UserId = userId });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
