using Microsoft.AspNetCore.Mvc;

namespace GymWeb.Controllers
{
    public class PerfilController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
