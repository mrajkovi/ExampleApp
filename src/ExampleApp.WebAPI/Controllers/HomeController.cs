
using Microsoft.AspNetCore.Mvc;

namespace ExampleApp.Controllers;
[Route("/")]
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult HomeView()
    {
      return View("Index");
    }
}