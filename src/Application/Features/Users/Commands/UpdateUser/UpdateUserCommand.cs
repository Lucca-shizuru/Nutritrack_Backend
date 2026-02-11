using MediatR;
using NutriTrack.src.Application.Common.Models;
using NutriTrack.src.Domain.Core;

namespace NutriTrack.src.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<Result<UserResponseDto>>
    {
        public Guid UserId { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
