using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
 
    }

    public async Task<User> Privacy()
    {   
        DbConnect db = new DbConnect();
        Task<User> asyncUser = db.GetUser( "admin", "initroot1234");
        User user = await asyncUser;

        return user;

       
    }
}


