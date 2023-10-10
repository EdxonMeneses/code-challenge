using DB.Contracts;
using DB.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DB.Services;

public class AuthService : IAuthService
{
    private const string TokenSecret = "this is my custom Secret key for authentication";
    private static readonly TimeSpan TokenLifeTime = TimeSpan.FromHours(24);
    public string BuildToken(TokenGenerationRequest tokenGenerationRequest)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(TokenSecret);

        var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, tokenGenerationRequest.Email),
                new(JwtRegisteredClaimNames.Email, tokenGenerationRequest.Email),
                new("userId", tokenGenerationRequest.UserId.ToString()),
            };
        if (tokenGenerationRequest.CustomClaims is not null)
        {
            foreach (var claimPair in tokenGenerationRequest.CustomClaims)
            {
                claims.Add(new Claim(claimPair.Key, claimPair.Value.ToString()));
            }
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TokenLifeTime),
            Issuer = "https://test.com",
            Audience = "https://test2.com",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        return jwt;
    }
}
