using MyMovie.Dtos;
using MyMovie.Services.Interface;
using System.Security.Claims;

namespace MyMovie.Services
{
    public class TokenService : ITokenService
    {
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            throw new NotImplementedException();
        }

        public string GetRefreshToken()
        {
            throw new NotImplementedException();
        }

        public TokenResponseDto GetToken(IEnumerable<Claim> claim)
        {
            throw new NotImplementedException();
        }
    }
}
