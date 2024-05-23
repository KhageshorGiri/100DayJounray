using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMovie.Data;
using MyMovie.Dtos;
using MyMovie.Services.Interface;

namespace MyMovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        private readonly MyMovieDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public TokenController(MyMovieDbContext ctx, ITokenService service)
        {
            _dbContext = ctx;
            _tokenService = service;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Refresh(RefreshTokenRequestDto tokenApiModel)
        {
            if (tokenApiModel is null)
                return BadRequest("Invalid client request");

            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity?.Name;

            var user = _dbContext.TokenInfo.SingleOrDefault(u => u.Usename == username);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry <= DateTime.Now)
                return BadRequest("Invalid client request");

            var newAccessToken = _tokenService.GetToken(principal.Claims);
            var newRefreshToken = _tokenService.GetRefreshToken();

            user.RefreshToken = newRefreshToken;
            _dbContext.SaveChanges(); 

            return Ok(new RefreshTokenRequestDto()
            {
                AccessToken = newAccessToken.TokenString,
                RefreshToken = newRefreshToken
            });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Revoke()
        {
            try
            {
                var username = User.Identity.Name;
                var user = _dbContext.TokenInfo.SingleOrDefault(u => u.Usename == username);
                if (user is null)
                    return BadRequest();
                user.RefreshToken = null;
                _dbContext.SaveChanges();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
