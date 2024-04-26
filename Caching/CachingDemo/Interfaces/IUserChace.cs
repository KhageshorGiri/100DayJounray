using CachingDemo.Models;

namespace CachingDemo.Interfaces
{
    public interface IUserChace
    {
        Task<IEnumerable<User>> GetUserFromCache();

        Task AdduserToCache(IEnumerable<User> user);
    }
}
