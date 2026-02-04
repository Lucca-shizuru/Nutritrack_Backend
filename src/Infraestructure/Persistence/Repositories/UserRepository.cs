using Microsoft.EntityFrameworkCore;
using NutriTrack.src.Domain.Entities;
using NutriTrack.src.Domain.Interfaces;

namespace NutriTrack.src.Infraestructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context; 

        public UserRepository (ApplicationDbContext context)
        {
            _context = context; 
        }
        public void Add(User user)
        {
            _context.Users.Add(user); 
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
           return await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }
        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.Meals) 
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
