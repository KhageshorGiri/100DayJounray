using ClientPro.Application.Dtos.Client;
using ClientPro.Application.Interfaces;
using ClientPro.Application.Mapping;
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

        public async Task<ClientDto> AddAsync(CreateClientDto client)
        {
            var clientToAdd = client.ToModel();
            clientToAdd.CreatedBy = 1;
            clientToAdd.CreateDate = DateTime.UtcNow.Date;
            clientToAdd.ModifiedBy = 1;
            clientToAdd.ModifiedDate = DateTime.UtcNow.Date;

            var response = await _clientRepository.AddAsync(clientToAdd);

            return response.ToClientDot();
        }

        public async Task<ClientDto> DeleteAsync(int id)
        {
            var clientToDelete = await _clientRepository.GetClientAsync(id);
            var response = await _clientRepository.DeleteAsync(clientToDelete);
            return response.ToClientDot();
        }

        public async Task<IEnumerable<ClientDto>> GetAllClientAsync()
        {
            var allClients = await _clientRepository.GetAllClientAsync();
            return allClients.Select(client => client.ToClientDot());
        }

        public async Task<ClientDto> GetClientAsync(int id)
        {
            var client = await _clientRepository.GetClientAsync(id);
            if(client is null)
            {
                return null;
            }
            return client.ToClientDot();
        }

        public async Task<ClientDto> UpdateAsync(int id, UpdateClientDto client)
        {
            var existingClient = await _clientRepository.GetClientAsync(id);

            existingClient.ClientName = client.ClientName;
            existingClient.Email = client.Email;
            existingClient.ModifiedBy = 2;
            existingClient.ModifiedDate = DateTime.UtcNow.Date;

            var response = await _clientRepository.UpdateAsync(existingClient);

            return response.ToClientDot();
        }
    }
}
