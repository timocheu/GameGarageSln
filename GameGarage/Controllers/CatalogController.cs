using GameGarage.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameGarage.Controllers;

public class CatalogController : Controller {
    private IGarageRepository _repository;

    public CatalogController(IGarageRepository repo) {
        _repository = repo;
    }
}
