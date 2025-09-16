using Microsoft.AspNetCore.Mvc;

namespace NutriTrack.src.Controllers
{
    public class MealsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
