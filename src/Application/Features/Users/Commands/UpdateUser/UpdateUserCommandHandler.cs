using MediatR;
using NutriTrack.src.Application.Common.Models;
using NutriTrack.src.Domain.Core;
using NutriTrack.src.Domain.Interfaces;

namespace NutriTrack.src.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<UserResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UserResponseDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
                return Result<UserResponseDto>.Failure("Usuário não encontrado.");

           
            user.UpdatePersonalInfo(request.Name, request.Email);

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            
            return Result<UserResponseDto>.Success(new UserResponseDto(
                user.Id,
                user.Name,
                user.Email,
                "Bearer (token mantido)"
            ));
        }
    }
}

