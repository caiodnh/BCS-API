using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Better_Call_Saul.Models;

namespace Better_Call_Saul.Controllers
{
    public class FavoriteCharactersController : Controller
    {
        private readonly BCSContext _context;

        public FavoriteCharactersController(BCSContext context)
        {
            _context = context;
        }

        // GET: FavoriteCharacters
        public async Task<IActionResult> Index()
        {
              return _context.FavoriteCharacters != null ? 
                          View(await _context.FavoriteCharacters.ToListAsync()) :
                          Problem("Entity set 'BCSContext.FavoriteCharacters'  is null.");
        }

        // GET: FavoriteCharacters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FavoriteCharacters == null)
            {
                return NotFound();
            }

            var favoriteCharacter = await _context.FavoriteCharacters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favoriteCharacter == null)
            {
                return NotFound();
            }

            return View(favoriteCharacter);
        }

        // GET: FavoriteCharacters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FavoriteCharacters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName")] FavoriteCharacter favoriteCharacter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(favoriteCharacter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(favoriteCharacter);
        }

        // GET: FavoriteCharacters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FavoriteCharacters == null)
            {
                return NotFound();
            }

            var favoriteCharacter = await _context.FavoriteCharacters.FindAsync(id);
            if (favoriteCharacter == null)
            {
                return NotFound();
            }
            return View(favoriteCharacter);
        }

        // POST: FavoriteCharacters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName")] FavoriteCharacter favoriteCharacter)
        {
            if (id != favoriteCharacter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favoriteCharacter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavoriteCharacterExists(favoriteCharacter.Id))
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
            return View(favoriteCharacter);
        }

        // GET: FavoriteCharacters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FavoriteCharacters == null)
            {
                return NotFound();
            }

            var favoriteCharacter = await _context.FavoriteCharacters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favoriteCharacter == null)
            {
                return NotFound();
            }

            return View(favoriteCharacter);
        }

        // POST: FavoriteCharacters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FavoriteCharacters == null)
            {
                return Problem("Entity set 'BCSContext.FavoriteCharacters'  is null.");
            }
            var favoriteCharacter = await _context.FavoriteCharacters.FindAsync(id);
            if (favoriteCharacter != null)
            {
                _context.FavoriteCharacters.Remove(favoriteCharacter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavoriteCharacterExists(int id)
        {
          return (_context.FavoriteCharacters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
