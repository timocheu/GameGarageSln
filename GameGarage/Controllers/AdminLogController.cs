using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameGarage.Models;

namespace GameGarage.Controllers
{
    [Authorize]
    public class AdminLogController : Controller
    {
        private readonly GameGarageDbContext _context;

        public AdminLogController(GameGarageDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 20;
            var logs = await _context.AuditLogs
                .OrderByDescending(l => l.Timestamp)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(await _context.AuditLogs.CountAsync() / (double)pageSize);

            return View(logs);
        }
    }
}
