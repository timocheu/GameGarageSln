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

    public IActionResult Category(int currentPage = 1)
    {
        return View(new CatalogListViewModel {
                Games = _repository.Games
                    .OrderBy(g => g.Id)
                    .Skip((currentPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo {
                    CurrentPage = currentPage,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Games.Count()
                }
            });
    }
}
