using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Data;
using AnimalShelter.Models;


namespace AnimalShelter.Controllers;

public class ContactController : Controller
{
    private readonly ApplicationDbContext _context;

    public ContactController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Submit(Contact contact)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.Contact!.Add(contact);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Your message has been sent successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error saving message: {ex.Message}");
            }
        }
        return View("Index", contact);
    }
}
