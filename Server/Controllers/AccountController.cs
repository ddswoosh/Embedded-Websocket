using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Entities;
using Server.Configurations;
using Server.Utils;

namespace Server.Controllers;

// [Authorize(Policy = "ValidateToken")]
public class AccountController : Controller
{
    private UserContext db;
    private Parser parser = new Parser();
    private JWT jwt = new JWT();
    public AccountController(UserContext db)
    {
        this.db = db;
    }
    public IActionResult Manage()
    {
        Configuration test = new Configuration();
        test.GetSecret();
        return View();   
    }
    
    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [AllowAnonymous]
    [Route("/Account/TryLogin")]
    [HttpPost]
    public async Task<string?> TryLogin()
    {
        StreamReader body = new StreamReader(HttpContext.Request.Body); 
        string[] json_user = parser.ParseStream(body);
        
        json_user[0] = json_user[0][1..(json_user[0].Length-1)];
        json_user[1] = json_user[1][1..(json_user[1].Length-1)];

        User[] entity_arr = db.GetUser(json_user);

        if (entity_arr == null)
        {
            return "Account not found, please new credentials or register your account.";
            
        }
        
        User entity = new User();
        entity.Username = entity_arr[0].Username;
        entity.Username = entity_arr[0].Password;

        string token = jwt.CreateToken(entity);
        return token;
        
    }
    
    [AllowAnonymous]
    [Route("/Account/TryRegister")]
    [HttpPost]
    public async Task<string> TryRegister()
    {
        StreamReader body = new StreamReader(HttpContext.Request.Body); 
        string[] json_user = parser.ParseStream(body);

        User entity = new User();
        entity.Username = json_user[0][1..(json_user[0].Length-1)];
        entity.Password = json_user[1][1..(json_user[1].Length-1)];
        entity.Type = json_user[2][1..(json_user[2].Length-1)];
        entity.API = json_user[3][1..(json_user[3].Length-1)];

        if (db.SetUser(json_user, entity))
        {
            return "Account was found with those credentials, please log in or sign up with a different username and password.";
        }   

        string token = jwt.CreateToken(entity);
        return token;
    }

}