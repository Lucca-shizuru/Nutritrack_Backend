using MediatR;

namespace NutriTrack.src.Application.Features.Users.Commands.RegisterUser
{
    public record RegisterUser : IRequest <int>
    {

    }
}
