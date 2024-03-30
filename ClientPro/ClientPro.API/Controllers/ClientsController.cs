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
        public async Task<IActionResult> Get()
        {
            var allClients = await _clientService.GetAllClientAsync();
            return Ok(allClients);
        }

        // GET : api/Clients/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var client = await _clientService.GetClientAsync(id);
            if(client is null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        // POST : api/Clients
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateClientDto client)
        {
            var response = await _clientService.AddAsync(client);
            return Ok(response);
        }

        // PUT : api/Clients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateClientDto client)
        {
            var existingClient = await _clientService.GetClientAsync(id);
            if(existingClient is null)
            {
                return NotFound();
            }
            var response = await _clientService.UpdateAsync(id, client);
            return Ok(response);
        }

        // DELETE : api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingClient = await _clientService.GetClientAsync(id);
            if (existingClient is null)
            {
                return NotFound();
            }
            var response = await _clientService.DeleteAsync(id);
            return Ok(response);
        }
    }
}
