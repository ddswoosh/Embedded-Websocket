using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers;

public class LiveController : Controller
{
    public IActionResult Select()
    {
        return View();
    }
    public IActionResult Pin1()
    {
        return View();
    }

    public IActionResult Pin2()
    {
        return View();
    }
    public IActionResult Pin3()
    {
        return View();
    }
    public IActionResult Pin4()
    {
        return View();
    }
    public IActionResult Pin5()
    {
        return View();
    }
    public IActionResult Pin6()
    {
        return View();
    }
    public IActionResult Pin7()
    {
        return View();
    }
    public IActionResult Pin8()
    {
        return View();
    }
}

[Route("api/pins/{id}")]
public class PinController() : Controller
{
    CurrentPin pinArr = new CurrentPin();
    [HttpPost]
    public void PutPin(string id) {
        pinArr.PutCurrentPin(id);
    }
}
