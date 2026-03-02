using Microsoft.AspNetCore.Mvc;

namespace GameGarage.Controllers;

public class HomeController : Controller {
    public IActionResult Index() => View();
}
