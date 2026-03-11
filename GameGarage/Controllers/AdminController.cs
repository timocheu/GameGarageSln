using Microsoft.AspNetCore.Mvc;

namespace GameGarage.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
