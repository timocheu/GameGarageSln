using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GameGarage.Models;
using GameGarage.Models.ViewModels;
using System.Linq;

namespace GameGarage.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IGarageRepository _repository;

        public AdminController(IGarageRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var allGames = _repository.Games.ToList();
            
            var viewModel = new DashboardViewModel
            {
                TotalGames = allGames.Count,
                FreeGamesCount = allGames.Count(g => g.Price == 0),
                AveragePrice = allGames.Any(g => g.Price > 0) ? allGames.Where(g => g.Price > 0).Average(g => g.Price) : 0,
                RecentlyAdded = allGames.OrderByDescending(g => g.ReleaseDate).Take(5).ToList(),
                TopPriceGames = allGames.OrderByDescending(g => g.Price).Take(3).ToList()
            };

            // Calculate category stats
            var categories = allGames
                .Where(g => !string.IsNullOrEmpty(g.Categories))
                .SelectMany(g => g.Categories!.Split(',', System.StringSplitOptions.RemoveEmptyEntries))
                .Select(c => c.Trim())
                .GroupBy(c => c)
                .ToDictionary(g => g.Key, g => g.Count());

            viewModel.CategoryStats = categories;
            viewModel.CategoryCount = categories.Count;

            return View(viewModel);
        }
    }
}
