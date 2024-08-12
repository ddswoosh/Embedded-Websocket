using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace Server.Entities;

public class JWT {

     // Eventually put key in AWS credential manager
    private const string Key = "3bXanrU5QUqjF6SNRi8khm8U+jNnF9ddBWy4qX4Et4s=";
    private const string Issuer = "dotnet-user-jwts";
    private const string Audience = "http://localhost:5203";
    private readonly JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

    public JWT()
    {
        
    }
    public string CreateToken(User entity)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var description = new JwtSecurityToken(

            issuer: Issuer,
            audience: Audience,
            claims: new List<Claim>
            {
                new Claim(ClaimTypes.Name, entity.Username),
                new Claim(ClaimTypes.Role, entity.Type),
            },
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        var token = handler.WriteToken(description);

        return token;
    }
    public bool ValidateToken(string token)
    {   
        // handler.ValidateTokenAsync(token, )
        return false; 
    }
}