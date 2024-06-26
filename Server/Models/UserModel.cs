using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Server.Models;

public class User : IdentityUser
{
    public string Username {get; set;}
    public string Password {get; set;}
    public string Type {get; set;}
    public string API {get; set;}

}