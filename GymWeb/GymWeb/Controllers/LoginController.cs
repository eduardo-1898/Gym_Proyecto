using Microsoft.AspNetCore.Mvc;

namespace GymWeb.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }

        public IActionResult RecuperarContrasenna()
        {
            return View();
        }


    }
}
