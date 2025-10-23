using MediatR;
using NutriTrack.src.Domain.Interfaces;
using NutriTrack.src.Application.Interfaces;
using NutriTrack.src.Domain.Entities;
using NutriTrack.src.Domain.Interfaces;

namespace NutriTrack.src.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly IUnitOfWork _unitOfWork;

        public Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
