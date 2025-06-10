using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace WapApi.Services
{
    public class TokinService
    {

        private readonly string _secretKey;
        private const int AccessTokenExpiryMinutes = 60; // 1 hour
        private const int RefreshTokenExpiryDays = 7; // 30 days

        public TokinService(IConfiguration Configuration)
        {
            _secretKey = Configuration["JwtSettings:Key"];
        }
        public string GenerateAccessToken(IEnumerable<Claim> claioms)
        {
            var Key  = new SymmetricSecurityKey(Encoding .UTF8.GetBytes(_secretKey));
            var credentials=new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
           var token  = new JwtSecurityToken(
           
                claims: claioms,
                expires: DateTime.UtcNow.AddMinutes(AccessTokenExpiryMinutes),
                signingCredentials: credentials
           );
            return  new JwtSecurityTokenHandler().WriteToken(token);    
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
