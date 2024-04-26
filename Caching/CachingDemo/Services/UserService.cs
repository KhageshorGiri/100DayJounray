using CachingDemo.Interfaces;
using CachingDemo.Models;

namespace CachingDemo.Services
{
    public class UserService : IUserService
    {
        private List<User> _users;
        public UserService()
        {
            _users = new List<User>()
            {
                new User{FullName = "Khageshor Giri", Email="girikhageshor@gmail.com"},
                new User{FullName = "Binod Giri", Email="giribinod@gmail.com"},
                new User{FullName = "Test User", Email="testuser@gmail.com"}
            };
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return _users;
        }
    }
}
