using NutriTrack.src.Domain.Entities;

namespace NutriTrack.src.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
        void Add(User user);
        void Update(User user);
    }
}
