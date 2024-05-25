using Microsoft.AspNetCore.Mvc;
using RequestService.Policies;

namespace RequestService.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class RquestController : ControllerBase
    {
        private ClientPolicy _clientPolicy;
        private IHttpClientFactory _httpClientFactory;

        public RquestController(ClientPolicy clientPolicy, IHttpClientFactory httpClientFactory)
        {
            _clientPolicy = clientPolicy;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductDetails([FromQuery] int id)
        {
            var client = _httpClientFactory.CreateClient();

            //var response = await client.GetAsync($"https://localhost:7156/api/Product/GetResponse?id={id}");

            var response = await _clientPolicy.ImmediateRertry.ExecuteAsync(
                () => client.GetAsync($"https://localhost:7156/api/Product/GetResponse?id={id}"));

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Sucess --> Requested.");
                return Ok(await response.Content.ReadAsStringAsync()) ;
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
