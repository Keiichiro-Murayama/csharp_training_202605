using Microsoft.AspNetCore.Mvc;

namespace src.Presentations.Controllers;

[Route("System")]
public class SystemController : Controller
{
    [HttpGet("Maintenance")]
    public IActionResult Maintenance()
    {
        return View();
    }
}
