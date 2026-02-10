using MediatR;
using NutriTrack.src.Domain.Core;

namespace NutriTrack.src.Application.Features.Users.Commands.LoginUser
{
    public record LoginUserCommand(string Email, string Password) : IRequest<Result<string>>;
    
    
}
