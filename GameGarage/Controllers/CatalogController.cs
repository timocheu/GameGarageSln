using GameGarage.Models;
using GameGarage.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameGarage.Controllers;

public class CatalogController : Controller
{
    private IGarageRepository _repository;
    public const int PageSize = 4;

    public CatalogController(IGarageRepository repo)
    {
        _repository = repo;
    }

    public IActionResult Category(string category = "All", int currentPage = 1)
    {
        var query = _repository.Games.AsQueryable();

        if (category != "All") {
            query = query.Where(g => g.Categories.Contains(category));
        }

        int totalItemsCount = query.Count();

        List<Game>? games = _repository.Games
                        .OrderBy(g => g.Id)
                        .Skip((currentPage - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();

        return View(new CatalogListViewModel {
                Games = games,
                PagingInfo = new PagingInfo {
                    CurrentPage = currentPage,
                    ItemsPerPage = PageSize,
                    TotalItems = totalItemsCount
                }
            });
    }
}
