using Microsoft.AspNetCore.Mvc;
using GameGarage.Models;

namespace GameGarage.Controllers;

public class HomeController : Controller {
    private IGarageRepository repository;

    public HomeController(IGarageRepository repo)
    {
        repository = repo;
    }


    public IActionResult Index() => View(repository.Games);
}
