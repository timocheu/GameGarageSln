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

    public IActionResult CategorySearch(string categoryInput = "All", int currentPage = 1)
    {
        // 1. Start with the full query
        IQueryable<Game> query = _repository.Games;

        // 2. Filter if it's not "All"
        if (categoryInput != "All")
        {
            // This translates to: WHERE Category LIKE '%Action%'
            // We use ToLower() on both sides to make it case-insensitive
            query = query.Where(g => g.Categories.ToLower().Contains(categoryInput.ToLower()));
        }

        // 3. Get the TOTAL count of filtered items for the pager
        int totalItemsCount = query.Count();

        // 4. Order, Page, and Execute
        var gamesList = query
            .OrderBy(g => g.Id)
            .Skip((currentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        return View("Search", new CatalogListViewModel
        {
            Games = gamesList,
            PagingInfo = new PagingInfo
            {
                CurrentPage = currentPage,
                ItemsPerPage = PageSize,
                TotalItems = totalItemsCount,
                Action = "CategorySearch"
            }
        });
    }

    public IActionResult TagSearch(string tagInput = "All", int currentPage = 1)
    {
        // 1. Initialize the query
        IQueryable<Game> query = _repository.Games;

        // 2. Apply the filter to the 'query' variable
        if (tagInput != "All")
        {
            query = query.Where(g => g.Tags.ToLower().Contains(tagInput.ToLower()));
        }

        // 3. Get the count from the 'query' (filtered)
        int totalItemsCount = query.Count();

        var games = query
                    .OrderBy(g => g.Id)
                    .Skip((currentPage - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();

        return View("Search", new CatalogListViewModel
        {
            Games = games,
            PagingInfo = new PagingInfo
            {
                CurrentPage = currentPage,
                ItemsPerPage = PageSize,
                TotalItems = totalItemsCount,
                Action = "TagSearch"
            }
        });
    }
}
