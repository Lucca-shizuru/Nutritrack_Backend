using NutriTrack.src.Domain.Entities;

namespace NutriTrack.src.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
