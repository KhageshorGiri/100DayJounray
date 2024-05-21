using JWTLearning.Models;
using JWTLearning.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWTLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly IJWTAuthenticationManager _tokenManager;
        public HomeController(IJWTAuthenticationManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        // GET: api/<HomeController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("hello");
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] Users user)
        {
            var token = await _tokenManager.Authenticate(user.Username, user.Password);
            if (token is null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
