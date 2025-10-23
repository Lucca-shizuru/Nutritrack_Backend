using MediatR;
using NutriTrack.src.Application.Interfaces;
using NutriTrack.src.Domain.Entities;
using NutriTrack.src.Domain.Interfaces;
using NutriTrack.src.Services;

namespace NutriTrack.src.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(
            IUserRepository userRepository,
            IPasswordHashingService passwordHashingService,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHashingService = passwordHashingService;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByEmailAsync(request.Email) is not null) {

                throw new InvalidOperationException("Este enail ja esta em uso.");

            }

            var passwordHash = _passwordHashingService.HashPassword(request.Password);

            var user = new User(request.Name, request.Email, passwordHash);

            _userRepository.Add(user);

            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

           
            return user.Id;


        }
    }
}
