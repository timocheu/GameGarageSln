using Microsoft.AspNetCore.Mvc;
using GameGarage.Models;
using Microsoft.EntityFrameworkCore;

namespace GameGarage.Controllers;

public class HomeController : Controller {
    private IGarageRepository repository;

    public HomeController(IGarageRepository repo)
    {
        repository = repo;
    }

    public IActionResult Index() {
        // Generate random pool view models
        var randomPool = repository.Games
            .OrderBy(g => EF.Functions.Random())
            .Take(30) 
            .ToArray();

        // Add into view model
        var viewModels = new HomeViewModel {
            Slides = randomPool.Take(4).ToArray(),
            Features = randomPool.Skip(4).Take(8).ToArray(),
            NewRelease = randomPool.Skip(12).Take(5).ToArray(),
            TopPlayerRated = randomPool.Skip(17).Take(5).ToArray(),
            MostPlayed = randomPool.Skip(22).Take(5).ToArray()
        };

        return View(viewModels);
    } 

    public IActionResult App(int id) {
        var app = repository.Games.FirstOrDefault(u => u.Id == id);

        return View(app);
    }
}
