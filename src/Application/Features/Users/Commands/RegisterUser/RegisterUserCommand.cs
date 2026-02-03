using MediatR;
using NutriTrack.src.Domain.Core;

namespace NutriTrack.src.Application.Features.Users.Commands.RegisterUser
{
    public record RegisterUserCommand : IRequest <Result<Guid>>
    {
        public required string Name { get; init; }
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
