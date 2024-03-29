using ClientPro.Application.Dtos.Client;
using ClientPro.Application.Interfaces;
using ClientPro.Domain.IRepositories;

namespace ClientPro.Application.Services
{
    public class ClientService : IClientService
    {

        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Task<ClientDto> AddAsync(CreateClientDto client)
        {
            throw new NotImplementedException();
        }

        public Task<ClientDto> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ClientDto>> GetAllClientAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ClientDto> GetClientAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ClientDto> UpdateAsync(UpdateClientDto client)
        {
            throw new NotImplementedException();
        }
    }
}
