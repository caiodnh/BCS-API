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
    public class FavoriteEpisodesController : Controller
    {
        private readonly BCSContext _context;

        public FavoriteEpisodesController(BCSContext context)
        {
            _context = context;
        }

        // GET: FavoriteEpisodes
        public async Task<IActionResult> Index()
        {
              return _context.FavoriteEpisodes != null ? 
                          View(await _context.FavoriteEpisodes.ToListAsync()) :
                          Problem("Entity set 'BCSContext.FavoriteEpisodes'  is null.");
        }

        // GET: FavoriteEpisodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FavoriteEpisodes == null)
            {
                return NotFound();
            }

            var favoriteEpisode = await _context.FavoriteEpisodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favoriteEpisode == null)
            {
                return NotFound();
            }

            return View(favoriteEpisode);
        }

        // GET: FavoriteEpisodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FavoriteEpisodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Season,NumberWithInSeason,Title")] FavoriteEpisode favoriteEpisode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(favoriteEpisode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(favoriteEpisode);
        }

        // GET: FavoriteEpisodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FavoriteEpisodes == null)
            {
                return NotFound();
            }

            var favoriteEpisode = await _context.FavoriteEpisodes.FindAsync(id);
            if (favoriteEpisode == null)
            {
                return NotFound();
            }
            return View(favoriteEpisode);
        }

        // POST: FavoriteEpisodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Season,NumberWithInSeason,Title")] FavoriteEpisode favoriteEpisode)
        {
            if (id != favoriteEpisode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favoriteEpisode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavoriteEpisodeExists(favoriteEpisode.Id))
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
            return View(favoriteEpisode);
        }

        // GET: FavoriteEpisodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FavoriteEpisodes == null)
            {
                return NotFound();
            }

            var favoriteEpisode = await _context.FavoriteEpisodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favoriteEpisode == null)
            {
                return NotFound();
            }

            return View(favoriteEpisode);
        }

        // POST: FavoriteEpisodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FavoriteEpisodes == null)
            {
                return Problem("Entity set 'BCSContext.FavoriteEpisodes'  is null.");
            }
            var favoriteEpisode = await _context.FavoriteEpisodes.FindAsync(id);
            if (favoriteEpisode != null)
            {
                _context.FavoriteEpisodes.Remove(favoriteEpisode);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavoriteEpisodeExists(int id)
        {
          return (_context.FavoriteEpisodes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
