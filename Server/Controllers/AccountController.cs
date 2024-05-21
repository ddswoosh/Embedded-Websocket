using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using Server.Models;

namespace Server.Controllers;

[Authorize(Policy = "DbAuth")]
public class AccountController : Controller
{
    private User _user;
    public AccountController(User user)
    {
        _user = user;
    }

    public IActionResult Manage()
    {
        if (_user.Auth == 1)
        {
            return View();   
        }

        return View("NotAuth");
    }
    
    [AllowAnonymous]
    public IActionResult Register()
    {
        if (_user.Auth == 1)
        {
            return View("Manage");
        }

        return View();   
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
        if (_user.Auth == 1)
        {
            return View("Manage");
        }

        return View();   
    }

    [AllowAnonymous]
    [Route("/Account/TryLogin")]
    [HttpPost]
    public void TryLogin()
    {
        int size = ((int)Request.Body.Length);
        byte[] buffer = new byte[size];
        IAsyncResult stream = Request.Body.BeginRead(buffer, 0, size, null, null);
        Console.WriteLine(stream);
    }
    
    [AllowAnonymous]
    [Route("/Account/TryRegister")]
    [HttpPost]
    public int TryRegister(string json)
    {
        // _db.PutUser(json)
        return 0;
    }
}
