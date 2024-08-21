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
    public AccountController(UserContext db, AWSSecrets secrets_manager)
    {
        this.db = db;
    }

    [Authorize]
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
    [Route("/TryLogin")]
    [HttpPost]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> TryLogin()
    {
        StreamReader body = new StreamReader(HttpContext.Request.Body); 
        string[] json_user = parser.ParseStream(body);
        
        json_user[0] = json_user[0][1..(json_user[0].Length-1)];
        json_user[1] = json_user[1][1..(json_user[1].Length-1)];

        User[]? entity_arr = db.GetUser(json_user);

        if (entity_arr == null)
        {
            return NotFound();
            
        }
        
        User entity = new User();
        entity.Username = entity_arr[0].Username;
        entity.Password = entity_arr[0].Password;
        entity.Type = entity_arr[0].Type;
    
        JWT jwt = new JWT();
        string token = await jwt.Create(entity);
        return Ok(token);
    }
    
    [AllowAnonymous]
    [Route("/TryRegister")]
    [HttpPost]
    public async Task<string?> TryRegister()
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
            return null;
        }   

        JWT jwt = new JWT();
        string token = await jwt.Create(entity);

        return token;
    }
}