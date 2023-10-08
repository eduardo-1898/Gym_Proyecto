using Microsoft.AspNetCore.Mvc;
using GymWeb.Models;
using GymWeb.Entities;

namespace GymWeb.Controllers
{
    public class LoginController : Controller
    {

        private readonly ILogger<LoginController> _logger;

        private readonly IUsuarioModel _usuarioModel;


        public LoginController(ILogger<LoginController> logger, IUsuarioModel usuarioModel)
        {
            _logger = logger;
            _usuarioModel = usuarioModel;
        }
        public IActionResult Login()
        {
            return View();
        }

      

        [HttpGet]
        public IActionResult RecuperarContrasenna()
        {
                     return View();
        }

        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuario(UsuarioEnt entidad)
        {
            var resp = _usuarioModel.RegistrarUsuario(entidad);
            if (resp == 1)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.MensajePantalla = "No se logro registrar el usuario";
                return View();

            }



        }

    }
}
