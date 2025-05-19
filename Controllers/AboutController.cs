using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter.Controllers;

public class AboutController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}