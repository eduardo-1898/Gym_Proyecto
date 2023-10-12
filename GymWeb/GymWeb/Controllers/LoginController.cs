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
        public IActionResult InicioSesion(UsuarioEnt entidad)
        {
            try
            {
                var datos = _usuarioModel.InicioSesion(entidad);

                if (datos?.Codigo != 1)
                {
                    ViewBag.Mensaje = datos?.Mensaje;
                    //REGISTRO DE BITACORA
                    //RegistrarEnBitacora("Usuario o Contraseña Incorrecta", entidad.USU_CONTRASENA.ToString(), "InicioSesion", 2);
                    //REGISTRO DE BITACORA
                    return View("Login");
                }

                HttpContext.Session.SetString("RolUsuario", datos.Objeto.NombreRol);

                //REGISTRO DE BITACORA
                //RegistrarEnBitacora("Inicio de Sesión", entidad.IdUsuario.ToString(), "HomeController", 1);
                //REGISTRO DE BITACORA

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Consulte con el administrador";
                return View("Login");
            }
        }

        [HttpPost]
        public IActionResult RegistrarUsuario(UsuarioEnt entidad)
        {
            try
            {
                var resp = _usuarioModel.RegistrarUsuario(entidad);
                if (resp == 1)
                    return RedirectToAction("Login", "Login");
                //Respuesta si no se logro registrar el usuario
                ViewBag.MensajePantalla = "No se logro registrar el usuario";
                return View();
            }
            catch (Exception)
            {
                ViewBag.MensajePantalla = "Ha ocurrido un error al procesar la solicitud de registro de usuario";
                return View();
            }
        }

    }
}
