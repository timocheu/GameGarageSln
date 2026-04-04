using Microsoft.AspNetCore.Mvc;
using GameGarage.Models;
using Microsoft.EntityFrameworkCore;

namespace GameGarage.Controllers;

public class HomeController : Controller
{
    private IGarageRepository repository;

    public HomeController(IGarageRepository repo)
    {
        repository = repo;
    }

    public IActionResult Index()
    {
        var games = repository.Games;
        
        var viewModels = new HomeViewModel
        {
            Slides = games.OrderBy(g => EF.Functions.Random()).Take(4).ToArray(),
            Features = games.OrderBy(g => EF.Functions.Random()).Take(8).ToArray(),
            NewRelease = games.AsEnumerable().OrderByDescending(g => g.ReleaseDate).ThenByDescending(g => g.Id).Take(5).ToArray(),
            CrossPlatform = games.Where(g => g.Windows == "True" && g.Mac == "True" && g.Linux == "True").Take(5).ToArray(),
            BudgetFriendly = games.Where(g => g.Price > 0).OrderBy(g => g.Price).Take(5).ToArray()
        };

        return View(viewModels);
    }

    public IActionResult About()
    {
        return View();
    }
}
