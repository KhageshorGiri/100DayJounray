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
                new User{Id = 1, FullName = "Khageshor Giri", Email="girikhageshor@gmail.com"},
                new User{Id = 2, FullName = "Binod Giri", Email="giribinod@gmail.com"},
                new User{Id = 3, FullName = "Test User", Email="testuser@gmail.com"}
            };
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return _users;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return _users.Where(user => user.Id == id).FirstOrDefault();
        }
    }
}
