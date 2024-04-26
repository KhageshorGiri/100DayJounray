using CachingDemo.Interfaces;
using CachingDemo.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CachingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserChace _userChace;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, IUserChace userChace, ILogger<UsersController> logger)
        {
            _userService = userService;
            _userChace = userChace;
            _logger = logger;

        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allUsers = await _userChace.GetUserFromCache();

            if(allUsers is null)
            {
                allUsers = await _userService.GetAllUserAsync();
                await _userChace.AdduserToCache(allUsers);
            }
            return Ok(allUsers);
        }

        // GET api/<UsersController>/5
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("Logging message");
            var client = await _userService.GetUserByIdAsync(id);
            return Ok(client);
        }

        // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
