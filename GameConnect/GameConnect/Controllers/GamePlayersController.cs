using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameConnect.Data;
using GameConnect.Models;
using Microsoft.AspNetCore.Authorization;

namespace GameConnect.Controllers
{
    public class GamePlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GamePlayersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GamePlayers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GamePlayer.Include(g => g.Game);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GamePlayers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GamePlayer == null)
            {
                return NotFound();
            }

            var gamePlayer = await _context.GamePlayer
                .Include(g => g.Game)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (gamePlayer == null)
            {
                return NotFound();
            }

            return View(gamePlayer);
        }

        // GET: GamePlayers/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Game, "GameId", "GameId");
            return View();
        }

        // POST: GamePlayers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("PostId,UserId,Name,Description,SocialMedia,GameId")] GamePlayer gamePlayer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gamePlayer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Game, "GameId", "GameId", gamePlayer.GameId);
            return View(gamePlayer);
        }

        // GET: GamePlayers/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GamePlayer == null)
            {
                return NotFound();
            }

            var gamePlayer = await _context.GamePlayer.FindAsync(id);
            if (gamePlayer == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Game, "GameId", "GameId", gamePlayer.GameId);
            return View(gamePlayer);
        }

        // POST: GamePlayers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,UserId,Name,Description,SocialMedia,GameId")] GamePlayer gamePlayer)
        {
            if (id != gamePlayer.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gamePlayer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamePlayerExists(gamePlayer.PostId))
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
            ViewData["GameId"] = new SelectList(_context.Game, "GameId", "GameId", gamePlayer.GameId);
            return View(gamePlayer);
        }

        // GET: GamePlayers/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GamePlayer == null)
            {
                return NotFound();
            }

            var gamePlayer = await _context.GamePlayer
                .Include(g => g.Game)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (gamePlayer == null)
            {
                return NotFound();
            }

            return View(gamePlayer);
        }

        // POST: GamePlayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GamePlayer == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GamePlayer'  is null.");
            }
            var gamePlayer = await _context.GamePlayer.FindAsync(id);
            if (gamePlayer != null)
            {
                _context.GamePlayer.Remove(gamePlayer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GamePlayerExists(int id)
        {
          return (_context.GamePlayer?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
    }
}
