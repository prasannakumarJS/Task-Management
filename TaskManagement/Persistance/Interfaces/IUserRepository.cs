using Domain.Entities;
namespace Persistance.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<User> GetAsync(Guid id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        System.Threading.Tasks.Task DeleteAsync(Guid id);
    }
}
