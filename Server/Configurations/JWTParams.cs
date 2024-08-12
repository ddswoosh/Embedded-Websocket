using Microsoft.IdentityModel.Tokens;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace Server.Configurations;

// Would be git ignored in production 
public class Configuration
{
    public static async Task GetSecret()
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

        string secret = response.SecretString;

        Console.WriteLine(secret);
    }
}
public class JwtSecurityTokenParameters
{
    // TokenValidationParameters JWTconfig = new TokenValidationParameters
    // {
    //     ValidIssuer = config["JWT:ValidIssuer"],
    //     ValidAudience = config["JWT:ValidAudience"],
    //     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"])),
    //     ValidateIssuer = true,
    //     ValidateAudience = true,
    //     ValidateLifetime = true,
    //     ValidateIssuerSigningKey = true
    // };
}