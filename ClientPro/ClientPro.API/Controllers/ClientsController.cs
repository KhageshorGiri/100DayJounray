using ClientPro.Application.Dtos.Client;
using ClientPro.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClientPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }


        // GET : api/Clients
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET : api/Clients/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST : api/Clients
        [HttpPost]
        public void Post([FromBody] CreateClientDto client)
        {
        }

        // PUT : api/Clients/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UpdateClientDto client)
        {
        }

        // DELETE : api/Clients/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
