using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMovie.Data;
using MyMovie.Dtos;
using MyMovie.Models;
using MyMovie.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

            if (user is not null)
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
            if (!result.Succeeded)
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
            // check whether user exist or not
            var user = await _userManager.FindByNameAsync(entity.Username);

            if (user is null || !await _userManager.CheckPasswordAsync(user, entity.Password))
                return Ok(new Status { StatusCode = 0, Message = "Invalid User name or Password." });

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GetToken(authClaims);
            var refreshToken = _tokenService.GetRefreshToken();
            var tokenInfo = _dbContext.TokenInfo.FirstOrDefault(a => a.Usename == user.UserName);

            if (tokenInfo == null)
            {
                var info = new TokenInfo
                {
                    Usename = user.UserName,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiry = DateTime.Now.AddDays(1)
                };
                _dbContext.TokenInfo.Add(info);
            }
            else
            {
                tokenInfo.RefreshToken = refreshToken;
                tokenInfo.RefreshTokenExpiry = DateTime.Now.AddDays(1);
            }

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new LoginResponseDto
            {
                Name = user.Name,
                Username = user.UserName,
                Token = token.TokenString,
                RefreshToken = refreshToken,
                Expiration = token.ValidTo,
                StatusCode = 1,
                Message = "Logged in"
            });
           
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ChnagePassword([FromBody] ChangePasswordDto model)
        {
            // check validations
            if (!ModelState.IsValid)
                return Ok(new Status { StatusCode = 0, Message = "please pass all the valid fields" });
            
            // lets find the user
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user is null)
                return Ok(new Status { StatusCode = 0, Message = "Invalid UserName" });
           
            // check current password
            if (!await _userManager.CheckPasswordAsync(user, model.CurrentPassword))
                return Ok(new Status { StatusCode = 0, Message = "Invalid Current Password." });

            // change password here
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
                return Ok(new Status { StatusCode = 0, Message = "Failed to change password." });

            return Ok(new Status { StatusCode = 0, Message = "Password has changed successfully" });
        }
    }
}
