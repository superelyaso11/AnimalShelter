using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;
using AnimalShelter.Data;
using Microsoft.EntityFrameworkCore;
namespace AnimalShelter.Controllers;

public class AnimalsController : Controller
{
    private readonly ApplicationDbContext _context;

    public AnimalsController(ApplicationDbContext context)
    {
        _context = context;
    }

//GET: Animals
public async Task<IActionResult> Index(string searchString)
{
//Always start with non-adopted animals
    var query = _context.Animals!
        .Where(a => !a.IsAdopted)
        .AsQueryable();

    if (!string.IsNullOrWhiteSpace(searchString))
    {
        query = query.Where(a => 
            a.Name.ToLower().Contains(searchString.Trim().ToLower())
        );
    }

    var animals = await query.ToListAsync();
    return View(animals);
}

//GET: Animals/Detail
public async Task<IActionResult> Detail(int? id)
{
    if (id == null || _context.Animals == null)
    {
        return NotFound();
    }

    var animal = await _context.Animals
        .FirstOrDefaultAsync(m => m.Id == id);
        
    if (animal == null)
    {
        return NotFound();
    }

    return View(animal);
}

//GET: Animals/Adopt
public async Task<IActionResult> Adopt(int? id)
{
    if (id == null || _context.Animals == null)
    {
        return NotFound();
    }

    var animal = await _context.Animals
        .AsNoTracking()
        .FirstOrDefaultAsync(m => m.Id == id);
        
    if (animal == null)
    {
        return NotFound();
    }

    var application = new AdoptionApplication
    {
        AnimalId = animal.Id,
        AnimalName = animal.Name ?? "Unknown Animal"
    };

    return View(application);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Adopt(int id, AdoptionApplication application)
{
    if (id != application.AnimalId)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            var animal = await _context.Animals!.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            if (animal.IsAdopted)
            {
            ModelState.AddModelError("", "This animal is already adopted.");
            return View(application);
            }

            animal.IsAdopted = true;
            _context.Update(animal);

            application.Animal = animal;
            application.AnimalName = animal.Name!;
            application.ApplicationDate = DateTime.UtcNow;

            _context.Add(application);
            await _context.SaveChangesAsync();
            
            TempData["SuccessMessage"] = "Application submitted successfully!";
            return RedirectToAction("AdoptionConfirmation", new { name = animal.Name });
        }
        catch (DbUpdateException ex)
        {
            ModelState.AddModelError("", $"Unable to save changes: {ex.Message}");
        }
    }
    return View(application);
}

public IActionResult AdoptionConfirmation(string name)
{
    if (string.IsNullOrEmpty(name))
    {
        name = TempData["AnimalName"]?.ToString() ?? "the animal";
    }
    
    ViewBag.AnimalName = name;
    return View();
}

public IActionResult Create()
{
    return View();
}

//POST: Animals/Create
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Animal animal)
{
    if (ModelState.IsValid)
    {
        if (animal.ImageFile != null && animal.ImageFile.Length > 0)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(animal.ImageFile.FileName);
            var filePath = Path.Combine("wwwroot/images", fileName);
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await animal.ImageFile.CopyToAsync(stream);
            }
            animal.Image = fileName;
        }

        _context.Add(animal);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = $"{animal.Name} added successfully!";
        return RedirectToAction(nameof(Index));
    }
    return View(animal);
}
}