using MediatR;
using NutriTrack.src.Application.Interfaces;
using NutriTrack.src.Domain.Core;
using NutriTrack.src.Domain.Interfaces;

namespace NutriTrack.src.Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<string>>
    {

        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashingService _passwordHasher;
        private readonly IJwtTokenGenerator _jwtGenerator;

        public LoginUserCommandHandler(
            IUserRepository userRepository,
            IPasswordHashingService passwordHasher,
            IJwtTokenGenerator jwtGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtGenerator = jwtGenerator;
        }
        public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user is null)
                return Result<string>.Failure("E-mail ou senha inválidos.");

         
            bool isPasswordValid = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);
            if (!isPasswordValid)
                return Result<string>.Failure("E-mail ou senha inválidos.");

            var token = _jwtGenerator.GenerateToken(user);

            return Result<string>.Success(token);
        }
    }
}
