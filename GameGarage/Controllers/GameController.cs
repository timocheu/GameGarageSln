using GameGarage.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameGarage.Controllers;

public class GameController : Controller {
    private readonly IGarageRepository _repository;

    public GameController(IGarageRepository repo) {
        _repository = repo;
    }

    public IActionResult Details(int id) {
        var app = _repository.Games.FirstOrDefault(u => u.Id == id);

        return View(app);
    }
}

