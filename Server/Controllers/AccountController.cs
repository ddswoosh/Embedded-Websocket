using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

public class AccountController : Controller
{
    public IActionResult Manage()
    {
        return View();
    }

}