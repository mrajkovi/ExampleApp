using Microsoft.AspNetCore.Mvc;

namespace ExampleApp.MVC.Controllers;

[Route("/")]
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult HomeView()
    {
      return View("Index");
    }
}