using System.Data.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Server.Models;

namespace Server.Controllers;

// [Authorize(Policy = "ValidateToken")]
public class AccountController : Controller
{ 
    private readonly DbContextOptions<UserContext> _db;

    public AccountController(DbContextOptions<UserContext> db)
    {
        _db = db;
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
    public async Task<string> TryLogin()
    {
        StreamReader body = new StreamReader(HttpContext.Request.Body); 
        string[] user = ReadJson(body);
        
        user[0] = user[0][1..(user[0].Length-1)];
        user[1] = user[1][1..(user[1].Length-1)];

        using (var db = new UserContext(_db))
        {  
            if (db.Users.Where(u => u.Username == user[0]).Where(p => p.Password == user[1]).ToArray().Length == 0)
            {   
                return "Account not found, please try again or resiger.";
            }

            else
            {
                return "ok";
                // create jwt and store client side
            }
        }
        return "t";
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

        using (var db = new UserContext(_db))
        {  
        if (db.Users.Where(u => u.Username == user[0]).Where(p => p.Password == user[1]).ToArray().Length > 0)
        {   
            return "Account was found with those credentials, please log in or sign up with a different username and password.";
        }

        else
        {
            db.Users.Add(entity);
            db.SaveChanges();

            return "Account created successfully";
        }
        
        }   
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