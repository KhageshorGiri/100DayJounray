using CachingDemo.Models;

namespace CachingDemo.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUserAsync();
    }
}
