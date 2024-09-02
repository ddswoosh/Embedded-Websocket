using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Amazon.SecretsManager.Endpoints;
using Amazon.SecretsManager.Model.Internal.MarshallTransformations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Server.Configurations;

namespace Server.Entities;

internal class JWT {

    private readonly JwtSecurityTokenParameters parameters = new JwtSecurityTokenParameters();
    private readonly AWSSecrets secrets_manager = new AWSSecrets();
    private readonly JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

    internal async Task<string> CreateJWT(User entity)
    {
        string[] secrets = await secrets_manager.GetSecret();
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrets[3]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var description = new JwtSecurityToken(
            issuer: secrets[2],
            audience: "http://localhost:5203",
            claims: new List<Claim>
            {
                new Claim(ClaimTypes.Name, entity.Type),
            },
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        var token = handler.WriteToken(description);

        return token;
    }

    internal async Task<bool> ValidateJWT(string token)
    {   
        TokenValidationParameters token_params = await parameters.ValidParams();

        SecurityToken validated_token;
        handler.ValidateToken(token, token_params, out validated_token);
        
        if (DateTime.UtcNow > validated_token.ValidTo)
        {
            return false;
        }
        // if (DateTime.UtcNow > validated_token.ValidFrom && DateTime.UtcNow < validated_token.ValidTo)
        // {
        //     var temp = handler.ReadJwtToken(token);
        //     List<Claim> arr = new List<Claim>(temp.Claims);

        //     return arr[0];
        // }

        return true;
    }
}

