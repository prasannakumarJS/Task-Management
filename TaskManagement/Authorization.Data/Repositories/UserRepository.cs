using TaskManagement.IDAM.Entities;
using Microsoft.EntityFrameworkCore;
using TaskManagement.IDAM.Interfaces;
using Authorization.Data;

namespace TaskManagement.IDAM.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(x=>x.UserName == username);
        }

        public async Task<User> GetUserWithUserNameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x=>x.UserName == username);
        }
    }

}
