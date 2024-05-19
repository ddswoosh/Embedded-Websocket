using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers;

public class PinController : Controller
{
    private PinLive _curPin;

    public PinController(PinLive curPin)
    {
        _curPin = curPin;
    }
    public IActionResult Select()
    {
        return View();
    }
    public IActionResult LivePin1() => View();
    public IActionResult LivePin2() => View();
    public IActionResult LivePin3() => View();
    public IActionResult LivePin4() => View();
    public IActionResult LivePin5() => View();
    public IActionResult LivePin6() => View();
    public IActionResult LivePin7() => View();
    public IActionResult LivePin8() => View();

    [Route("api/pins/{id}")]
    [HttpPost]
    public void PutPin(string id) {
        _curPin.PutCurrentPin(id);
    }
    
    public IActionResult GetPin()
    {
        Thread.Sleep(500);
        return View(_curPin.GetCurrentPin());
    }
}
