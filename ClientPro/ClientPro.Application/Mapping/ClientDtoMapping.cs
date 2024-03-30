using ClientPro.Application.Dtos.Client;
using ClientPro.Domain.Entities;

namespace ClientPro.Application.Mapping
{
    public static class ClientDtoMapping
    {
        public static ClientDto ToClientDot(this Client client)
        {
            return new()
            {
                Id = client.Id,
                ClientName = client.ClientName,
                Email = client.Email,
            };
        }

        public static Client ToModel(this ClientDto client)
        {
            return new()
            {
                Id = client.Id,
                ClientName = client.ClientName,
                Email = client.Email,
            };
        }
    }

    public static class CreateClientDtoMapping
    {
        public static CreateClientDto ToCreateClientDot(this Client client)
        {
            return new()
            {
                ClientName = client.ClientName,
                Email = client.Email,
            };
        }

        public static Client ToModel(this CreateClientDto client)
        {
            return new()
            {
                ClientName = client.ClientName,
                Email = client.Email,
            };
        }
    }

    public static class UpdateClientDtoMapping
    {
        public static UpdateClientDto ToCreateClientDot(this Client client)
        {
            return new()
            {
                ClientName = client.ClientName,
                Email = client.Email,
            };
        }

        public static Client ToModel(this UpdateClientDto client)
        {
            return new()
            {
                ClientName = client.ClientName,
                Email = client.Email,
            };
        }
    }
}
