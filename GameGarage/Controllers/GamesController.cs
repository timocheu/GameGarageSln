using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameGarage.Models.ViewModels;
using GameGarage.Models;

namespace GameGarage.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameGarageDbContext _context;
        private readonly GameGarage.Infrastructure.IAuditService _auditService;

        int PageSize = 20;

        public GamesController(GameGarageDbContext context, GameGarage.Infrastructure.IAuditService auditService)
        {
            _context = context;
            _auditService = auditService;
        }

        // GET: Games
        [Authorize]
        public async Task<IActionResult> Index(string searchString, string sortOrder, int currentPage = 1)
        {
            var gamesQuery = _context.Games.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                gamesQuery = gamesQuery.Where(g => g.Name.Contains(searchString) 
                                                || (g.Developers != null && g.Developers.Contains(searchString)) 
                                                || g.Id.ToString() == searchString);
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;

            gamesQuery = sortOrder switch
            {
                "name_desc" => gamesQuery.OrderByDescending(g => g.Name),
                "price" => gamesQuery.OrderBy(g => g.Price),
                "price_desc" => gamesQuery.OrderByDescending(g => g.Price),
                "date" => gamesQuery.OrderBy(g => g.ReleaseDate),
                "date_desc" => gamesQuery.OrderByDescending(g => g.ReleaseDate),
                _ => gamesQuery.OrderBy(g => g.Name),
            };

            var totalItems = await gamesQuery.CountAsync();

            var games = await gamesQuery
                .Skip((currentPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            return View(new CatalogListViewModel
            {
                Games = games,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = currentPage,
                    ItemsPerPage = PageSize,
                    TotalItems = totalItems,
                    Action = "Index"
                }
            });
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ReleaseDate,Price,AboutTheGame,Notes,Developers,Publishers,Categories,HeaderImage,Screenshots,Tags,Windows,Mac,Linux")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                await _auditService.LogAction(User.Identity?.Name ?? "Admin", "Add Game", $"Added game '{game.Name}' (ID: {game.Id})");
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Games/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ReleaseDate,Price,AboutTheGame,Notes,Developers,Publishers,Categories,HeaderImage,Screenshots,Tags,Windows,Mac,Linux")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                    await _auditService.LogAction(User.Identity?.Name ?? "Admin", "Edit Game", $"Updated game details for '{game.Name}' (ID: {game.Id})");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
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
            return View(game);
        }

        // GET: Games/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                var gameName = game.Name;
                var gameId = game.Id;
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
                await _auditService.LogAction(User.Identity?.Name ?? "Admin", "Delete Game", $"Deleted game '{gameName}' (ID: {gameId})");
            }
            else
            {
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
