using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers;

// [Authorize(Policy = "ValidateToken")]
public class AccountController : Controller
{

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
        DbConnect db = new DbConnect();

        StreamReader body = new StreamReader(HttpContext.Request.Body); 
        Task<string> buffer = body.ReadToEndAsync();
        string unwrap = buffer.Result;

        int size = unwrap.Length - 1;
        unwrap = unwrap[1..size];

        Dictionary<string, string> json = unwrap
        .Split(',')
        .Select (part  => part.Split(':'))
        .ToDictionary (sp => sp[0], sp => sp[1]);

        string[] user = new string[2];
        int i = 0;

        foreach(KeyValuePair<string, string> temp in json) 
        {
            user[i] = temp.Value;
            i++;
        }
        
        user[0] = user[0][1..(user[0].Length-1)];
        user[1] = user[1][1..(user[1].Length-1)];
        var res = db.GetUser(user[0], user[1]);
        User test= res.Result;

        return test.Username;
        // Success
        //  - render acc manage view
        //  - cache jwt client side

        // Failure
        //  - pop up Invalid credentials 
        
        
    
    }
    
    [AllowAnonymous]
    [Route("/Account/TryRegister")]
    [HttpPost]
    public async Task<string> TryRegister()
    {
        DbConnect db = new DbConnect();

        StreamReader body = new StreamReader(HttpContext.Request.Body); 
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

        user[0] = user[0][1..(user[0].Length-1)];
        user[1] = user[1][1..(user[1].Length-1)];
        user[2] = user[2][1..(user[2].Length-1)];
        user[3] = user[3][1..(user[3].Length-1)];

        if (user[3].Length != 20) 
        {  
            return "API length not satisfied, please click gen and then submit";
        }
        else 
        {
            return db.PutUser(user[0], user[1], user[2], user[3]).Result;
        }
    }
}