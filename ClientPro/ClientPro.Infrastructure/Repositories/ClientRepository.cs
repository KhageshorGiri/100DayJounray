using ClientPro.Domain.Entities;
using ClientPro.Domain.IRepositories;
using ClientPro.Infrastructure.DataContext;

namespace ClientPro.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {

        private readonly ClientDbContext _dbContext;

        public ClientRepository(ClientDbContext context)
        {
            _dbContext = context;
        }

        public Task<Client> AddAsync(Client client)
        {
            throw new NotImplementedException();
        }

        public Task<Client> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Client>> GetAllClientAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Client> GetClientAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Client> UpdateAsync(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
