using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter.Controllers;

public class ContactController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
