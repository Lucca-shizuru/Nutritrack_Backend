using MediatR;
using NutriTrack.src.Application.Interfaces;
using NutriTrack.src.Domain.Entities;
using NutriTrack.src.Domain.Interfaces;
using NutriTrack.src.Services;
using NutriTrack.src.Domain.Core;

namespace NutriTrack.src.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
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

        public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _userRepository.GetByEmailAsync(request.Email) is not null;
            if (userExists)
                return Result<Guid>.Failure("Este e-mail já está em uso.");

           
            var passwordHash = _passwordHashingService.HashPassword(request.Password);
            var user = new User(request.Name, request.Email, passwordHash);

            
            _userRepository.Add(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(user.Id);
        }
    }
}
