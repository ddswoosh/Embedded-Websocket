using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Internal;
using Server.Models;

namespace Server.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    private User _user;
    public HomeController(User user){
        _user = user;
    }   
    public IActionResult Index()
    {
        return View();
 
    }

    public IActionResult Privacy()
    {   
        DbConnect db = new DbConnect(_user);
        db.GetUser( "admin", "initroot1234");

        // Console.WriteLine(_user.Username);

        return View();
    }
}


