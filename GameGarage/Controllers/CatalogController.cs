using GameGarage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameGarage.Controllers;

public class CatalogController : Controller
{
    private IGarageRepository _repository;

    public CatalogController(IGarageRepository repo)
    {
        _repository = repo;
    }

    public IActionResult Category(string category)
    {
        var result = _repository.Games
            .Where(u => u.Categories.Contains(category))
            .OrderBy(g => EF.Functions.Random())
            .Take(5)
            .ToArray();

        CategoryViewModel viewModel = new CategoryViewModel()
        {
            Games = result,
            Category = category
        };

        return View("Category", viewModel);
    }
}
