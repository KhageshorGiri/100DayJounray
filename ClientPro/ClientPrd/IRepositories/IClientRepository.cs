using ClientPro.Domain.Entities;

namespace ClientPro.Domain.IRepositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllClientAsync();
        Task<Client> GetClientAsync(int id);
        Task<Client> AddAsync(Client client);
        Task<Client> UpdateAsync(Client client);
        Task<Client> DeleteAsync(int id);
    }
}
