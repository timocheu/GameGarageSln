using GameGarage.Models;
using GameGarage.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameGarage.Controllers;

public class CatalogController : Controller
{
    private IGarageRepository _repository;
    public const int PageSize = 4;

    public CatalogController(IGarageRepository repo)
    {
        _repository = repo;
    }

    public IActionResult Index(string searchString, string category = "All", string sortOrder = "name", int currentPage = 1)
    {
        IQueryable<Game> query = _repository.Games;

        // 1. Filter by Search String (Case-Insensitive & Null-Safe)
        if (!string.IsNullOrEmpty(searchString))
        {
            var searchLower = searchString.ToLower();
            query = query.Where(g => (!string.IsNullOrEmpty(g.Name) && g.Name.ToLower().Contains(searchLower)) 
                                || (!string.IsNullOrEmpty(g.Developers) && g.Developers.ToLower().Contains(searchLower)));
        }

        // 2. Filter by Category
        if (category != "All")
        {
            var categoryLower = category.ToLower();
            query = query.Where(g => !string.IsNullOrEmpty(g.Categories) && g.Categories.ToLower().Contains(categoryLower));
        }

        // 3. Sorting logic
        ViewData["CurrentSort"] = sortOrder;
        query = sortOrder switch
        {
            "name_desc" => query.OrderByDescending(g => g.Name),
            "price" => query.OrderBy(g => g.Price),
            "price_desc" => query.OrderByDescending(g => g.Price),
            "date" => query.OrderBy(g => g.ReleaseDate),
            "date_desc" => query.OrderByDescending(g => g.ReleaseDate),
            _ => query.OrderBy(g => g.Name),
        };

        // 4. Count Total Items for Pager
        int totalItemsCount = query.Count();

        // 5. Order and Page results
        var gamesList = query
            .Skip((currentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        ViewData["CurrentFilter"] = searchString;
        ViewData["CurrentCategory"] = category;

        // Fetch unique categories for the sidebar in-memory
        ViewBag.Categories = _repository.Games
            .Where(g => !string.IsNullOrEmpty(g.Categories))
            .Select(g => g.Categories)
            .AsEnumerable() // Pulled into memory to handle the split/distinct logic
            .SelectMany(c => c!.Split(',', System.StringSplitOptions.RemoveEmptyEntries))
            .Select(c => c.Trim())
            .Distinct()
            .OrderBy(c => c)
            .ToList();

        return View(new CatalogListViewModel
        {
            Games = gamesList,
            PagingInfo = new PagingInfo
            {
                CurrentPage = currentPage,
                ItemsPerPage = PageSize,
                TotalItems = totalItemsCount,
                Action = "Index"
            }
        });
    }

    public IActionResult CategorySearch(string categoryInput = "All", int currentPage = 1)
    {
        return RedirectToAction("Index", new { category = categoryInput, currentPage });
    }

    public IActionResult TagSearch(string tagInput = "All", int currentPage = 1)
    {
        // For tags, we can just use the searchString or add a tag parameter to Index.
        // For now, let's just search the tags in Index if we want.
        return RedirectToAction("Index", new { searchString = tagInput, currentPage });
    }
}
