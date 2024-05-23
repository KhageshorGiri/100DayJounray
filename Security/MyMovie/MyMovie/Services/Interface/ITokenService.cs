using MyMovie.Dtos;
using System.Security.Claims;

namespace MyMovie.Services.Interface
{
    public interface ITokenService
    {
        TokenResponseDto GetToken(IEnumerable<Claim> claim);
        string GetRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
