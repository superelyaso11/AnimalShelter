using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Data;
using AnimalShelter.Models;


namespace AnimalShelter.Controllers
{
    public class DonationsController : Controller
{
    private readonly ApplicationDbContext _context;

    public DonationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    //GET: Donations
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(Donation donation)
    {
        if (ModelState.IsValid)
        {
            _context.Add(donation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Thanks");
        }
        return View(donation);
    }

    public IActionResult Thanks()
    {
        return View();
    }
}
}
