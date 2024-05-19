using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using Server.Models;

namespace Server.Controllers;

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
    
    public IActionResult Register()
    {
        if (_user.Auth == 1)
        {
            return View("Manage");
        }

        return View();   
    }

    public IActionResult Login()
    {
        if (_user.Auth == 1)
        {
            return View("Manage");
        }

        return View();   
    }

    [Route("/Account/TryLogin")]
    [HttpPost]
    public void TryLogin()
    {
        int size = ((int)Request.Body.Length);
        byte[] buffer = new byte[size];
        IAsyncResult stream = Request.Body.BeginRead(buffer, 0, size, null, null);
        Console.WriteLine(stream);
    }
    
    [Route("/Account/TryRegister")]
    [HttpPost]
    public int TryRegister(string json)
    {
        User user = new User();
        return 0;
    }
}
