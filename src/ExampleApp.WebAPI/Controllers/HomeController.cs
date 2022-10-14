
using Microsoft.AspNetCore.Mvc;

namespace ExampleApp.WebAPI.Controllers;
[Route("/")]
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult HomeView()
    {
      return View("Index");
    }
}