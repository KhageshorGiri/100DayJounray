using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMovie.Data;
using MyMovie.Dtos;
using MyMovie.Models;
using MyMovie.Services.Interface;

namespace MyMovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly MyMovieDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(MyMovieDbContext context, 
            ITokenService tokenService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _dbContext = context;
            _tokenService = tokenService;
            _userManager = userManager;
            _roleManager = roleManager;

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Registration([FromBody] RegistrationDto registrationDto)
        {

            if (!ModelState.IsValid)
                return Ok(new Status { StatusCode = 0, Message = "Please Enter Valid Value." });

            // check if user alreay exist or not
            var user = await _userManager.FindByNameAsync(registrationDto.Username);

            if(user is not null)
                return Ok(new Status { StatusCode = 0, Message = "User already exist." });

            var newUser = new ApplicationUser()
            {
                UserName = registrationDto.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = registrationDto.Email,
                Name = registrationDto.Name
            };
            // add user to db
            var result = await _userManager.CreateAsync(newUser);
            if(!result.Succeeded)
                return Ok(new Status { StatusCode = 0, Message = "Unable to add new user. Opertion Failed." });

            // add role with uer
            // check if role exist or not
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.User))
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return Ok(new Status { StatusCode = 1, Message = "Registration Completed." });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginDto entity)
        {
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ChnagePassword([FromBody] ChangePasswordDto entity)
        {
            return Ok();
        }
    }
}
