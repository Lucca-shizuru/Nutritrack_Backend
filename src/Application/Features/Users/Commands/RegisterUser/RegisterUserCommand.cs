using MediatR;

namespace NutriTrack.src.Application.Features.Users.Commands.RegisterUser
{
    public record RegisterUserCommand : IRequest <int>
    {
        public required string Name { get; init; }
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
