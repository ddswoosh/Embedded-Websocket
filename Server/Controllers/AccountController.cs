using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers;

// [Authorize(Policy = "ValidateToken")]
public class AccountController : Controller
{
    private UserContext db;
    private JWT jwt = new JWT();
    public AccountController(UserContext db)
    {
        this.db = db;
    }
    public IActionResult Manage()
    {
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
        string[] user = ReadJson(body);
        
        user[0] = user[0][1..(user[0].Length-1)];
        user[1] = user[1][1..(user[1].Length-1)];

        User[] entity = db.GetUser(user);

        if (entity == null)
        {
            return "Account not found, please new credentials or register your account.";
            
        }

        string info = jwt.CreateToken(entity);
        return info;
        
    }
    
    [AllowAnonymous]
    [Route("/Account/TryRegister")]
    [HttpPost]
    public async Task<string> TryRegister()
    {
        StreamReader body = new StreamReader(HttpContext.Request.Body); 
        string[] user = ReadJson(body);

        User entity = new User();
        entity.Username = user[0][1..(user[0].Length-1)];
        entity.Password = user[1][1..(user[1].Length-1)];
        entity.Type = user[2][1..(user[2].Length-1)];
        entity.API = user[3][1..(user[3].Length-1)];

        if (db.SetUser(user, entity))
        {
            return "Account was found with those credentials, please log in or sign up with a different username and password.";
        }

        // Create JWT and return it as string, store local client storage
        return "Account created successfully";
    }

    public string[] ReadJson(StreamReader body)
    {
        Task<string> buffer = body.ReadToEndAsync();
        string unwrap = buffer.Result;

        int size = unwrap.Length - 1;
        unwrap = unwrap[1..size];

        Dictionary<string, string> json = unwrap
        .Split(',')
        .Select (part  => part.Split(':'))
        .ToDictionary (sp => sp[0], sp => sp[1]);

        string[] user = new string[4];
        int i = 0;

        foreach(KeyValuePair<string, string> temp in json)
        {
            user[i] = temp.Value;
            i++;
        }

        return user;
    }
}