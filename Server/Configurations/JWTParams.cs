using Microsoft.IdentityModel.Tokens;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Server.Utils;
using System.Text;


namespace Server.Configurations;

public class AWSSecrets
{
    private Parser parser = new Parser();
    public async Task<string[]> GetSecret()
    {
        string secretName = "embedded/configuration";
        string region = "us-east-1";

        IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

        GetSecretValueRequest request = new GetSecretValueRequest
        {
            SecretId = secretName,
            
            VersionStage = "AWSCURRENT",
        };

        GetSecretValueResponse response;

        try
        {
            response = await client.GetSecretValueAsync(request);
        }

        catch (Exception e)
        {
            throw e;
        }

        return parser.ParseJson(response.SecretString);  
    }
}
internal class JwtSecurityTokenParameters : TokenValidationParameters
{
    private readonly AWSSecrets secrets_manger = new AWSSecrets();

    internal async Task<TokenValidationParameters> ValidParams()
    {
        string[] secrets = await secrets_manger.GetSecret();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrets[3]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        TokenValidationParameters JWTconfig = new TokenValidationParameters
        {
        
        ValidIssuer = secrets[2],
        ValidAudience = "http://localhost:5203",
        IssuerSigningKey = key,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        };

        return JWTconfig;
    }
    
}