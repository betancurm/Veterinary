using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Veterinary.web.Data;
using Veterinary.web.Models;

namespace Veterinary.web.Controllers
{
    public class OwnersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OwnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Owners
        public async Task<IActionResult> Index()
        {
            return View(await _context.Owners.Include(c => c.Pets).ToListAsync());
        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Owner owner = await _context.Owners
            .Include(c => c.Pets)
            .ThenInclude(d => d.Veterinarians)
            .FirstOrDefaultAsync(m => m.Id == id);
            if (owner == null)
            {
                return NotFound();
            }
            return View(owner);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Identification,Address,Phone,Email,Neighborhood")] Owner owner)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(owner);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "hay un registro con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                        dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(owner);
        }

        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }
            return View(owner);
        }

        // POST: Owners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("Id,FullName,Identification,Address,Phone,Email,Neighborhood")] Owner owner)
        {
            if (Id != owner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(owner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnerExists(owner.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(owner);
        }

        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Owner owner = await _context.Owners
            .Include(c => c.Pets)
            .ThenInclude(d=>d.Veterinarians)
            .FirstOrDefaultAsync(m=>m.Id==id);
            if (owner == null)
            {
                return NotFound();
            }
            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnerExists(int id)
        {
            return _context.Owners.Any(e => e.Id == id);
        }
        //Add Pets
        public async Task<IActionResult> AddPet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Owner owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }
            Pet model = new Pet { IdOwner = owner.Id };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPet(Pet pet)
        {
            
        if (ModelState.IsValid)
            {
                Owner owner = await _context.Owners.Include(c => c.Pets).FirstOrDefaultAsync(c => c.Id == pet.IdOwner);
                if (owner == null)
                {
                    return NotFound();
                }
                try
                {
                    pet.Id = 0;
                    owner.Pets.Add(pet);
                    _context.Update(owner);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new
                    {
                        Id = owner.Id
                    });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if
                    (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                        dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(pet);
        }
        //Edit Pets
        public async Task<IActionResult> EditPet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Pet pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            Owner owner = await _context.Owners.FirstOrDefaultAsync(c =>
            c.Pets.FirstOrDefault(d => d.Id == pet.Id) != null);
            pet.IdOwner = owner.Id;
            return View(pet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPet(Pet pet)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pet);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new
                    {
                        Id = pet.IdOwner
                    });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                        dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    
                ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(pet);
        }

        // Delete Pets
        public async Task<IActionResult> DeletePet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Pet pet = await _context.Pets
            .Include(d => d.Veterinarians)
            .FirstOrDefaultAsync(m => m.Id == id);
            if (pet == null)
            {
                return NotFound();
            }
            Owner owner = await _context.Owners.FirstOrDefaultAsync(c =>
            c.Pets.FirstOrDefault(d => d.Id == pet.Id) != null);
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = owner.Id });
        }
        //Details Pets

        public async Task<IActionResult> DetailsPet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Pet pet = await _context.Pets
            .Include(d => d.Veterinarians)
            .FirstOrDefaultAsync(m => m.Id == id);
            if (pet == null)
            {
                return NotFound();
            }
            Owner owner = await _context.Owners.FirstOrDefaultAsync(c =>
            c.Pets.FirstOrDefault(d => d.Id == pet.Id) != null);
            pet.IdOwner = owner.Id;
            return View(pet);
        }

        // Add Veterinarian

        public async Task<IActionResult> AddVeterinarian(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Pet pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {

                return NotFound();
            }
            Veterinarian model = new Veterinarian { IdPet = pet.Id };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVeterinarian(Veterinarian veterinarian)
        {
            if (ModelState.IsValid)
            {
                Pet pet = await _context.Pets
                .Include(d => d.Veterinarians)
                .FirstOrDefaultAsync(c => c.Id == veterinarian.IdPet);
                if (pet == null)
                {
                    return NotFound();
                }
                try
                {
                    veterinarian.Id = 0;
                    pet.Veterinarians.Add(veterinarian);
                    _context.Update(pet);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(DetailsPet), new { Id = pet.Id });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                        dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(veterinarian);
        }

        //Edit Veterinarian
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVeterinarian(Veterinarian veterinarian)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veterinarian);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(DetailsPet), new { Id = veterinarian.IdPet });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                        dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(veterinarian);
        }
        public async Task<IActionResult> EditVeterinarian(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Veterinarian veterinarian = await _context.Veterinarians.FindAsync(id);
            if (veterinarian == null)
            {
                return NotFound();
            }
            Pet pet = await _context.Pets.FirstOrDefaultAsync(d =>
            d.Veterinarians.FirstOrDefault(c => c.Id == veterinarian.Id) != null);
            veterinarian.IdPet = pet.Id;
            return View(veterinarian);
        }
        //Delete Veterinarian
        public async Task<IActionResult> DeleteVeterinarian(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Veterinarian veterinarian = await _context.Veterinarians
            .FirstOrDefaultAsync(m => m.Id == id);
            if (veterinarian == null)
            {
                return NotFound();
            }
            Pet pet = await _context.Pets.FirstOrDefaultAsync(d
            => d.Veterinarians.FirstOrDefault(c => c.Id == veterinarian.Id) != null);
            _context.Veterinarians.Remove(veterinarian);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DetailsPet), new { Id = pet.Id });
        }
    }
}
