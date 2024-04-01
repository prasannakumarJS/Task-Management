using TaskManagement.IDAM.Entities;
namespace TaskManagement.IDAM.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<bool> UserExistsAsync(string username);
        Task<User> GetUserWithUserNameAsync(string username);
    }
}
