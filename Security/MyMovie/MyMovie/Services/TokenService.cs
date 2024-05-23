using Microsoft.IdentityModel.Tokens;
using MyMovie.Dtos;
using MyMovie.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MyMovie.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            //principal
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters,  out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        public string GetRefreshToken()
        {
            var raddomNumber = new byte[64];
            using (var rang = RandomNumberGenerator.Create())
            {
                rang.GetBytes(raddomNumber);
                return Convert.ToBase64String(raddomNumber);
            }
        }

        public TokenResponseDto GetToken(IEnumerable<Claim> claim)
        {
            var key = _configuration["JWT:Secret"];
            var privatekey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                expires: DateTime.Now.AddMinutes(30),
                claims: claim,
                signingCredentials: new SigningCredentials(privatekey, SecurityAlgorithms.HmacSha256));

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenResponseDto { TokenString = tokenValue, ValidTo = token.ValidTo};
        }
    }
}
