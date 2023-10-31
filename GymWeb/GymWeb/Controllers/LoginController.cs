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

        [HttpPost]
        public IActionResult InicioSesion(UsuarioEnt entidad)
        {
            try
            {
                var datos = _usuarioModel.InicioSesion(entidad);

                if (datos?.Codigo != 1)
                {
                    ViewBag.Mensaje = datos?.Mensaje;

                    ViewBag.Mensaje = "Consulte con el administrador";
                    //REGISTRO DE BITACORA
                    //RegistrarEnBitacora("Usuario o Contraseña Incorrecta", entidad.USU_CONTRASENA.ToString(), "InicioSesion", 2);
                    //REGISTRO DE BITACORA

                    return View("Index");
                }

                HttpContext.Session.SetString("RolUsuario", datos.Objeto.IdRol.ToString());

                //REGISTRO DE BITACORA
                //RegistrarEnBitacora("Inicio de Sesión", entidad.IdUsuario.ToString(), "HomeController", 1);
                //REGISTRO DE BITACORA

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ViewBag.Mensaje = "Consulte con el administrador";
                return View("Index");
            }
        }

        public IActionResult Index()
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

        //Inicio metodo de recuperacion
        [HttpGet]
        public IActionResult RecuperarContrasenna()
        {
                     return View();
        }
           

        [HttpPost]
        public IActionResult RecuperarContrasenna(UsuarioEnt entidad)
        {
            var resp = _usuarioModel.RecuperarContrasenna(entidad);
            if (resp == 1)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.MensajePantalla = "No se logro recuperar el usuario";
                return View();

            }
        }

        //Fin metodo de recuperacion

        //Inicio metodo de cambiar clave

        [HttpGet]
        public IActionResult CambiarClaveCuenta(string q)
        {
            UsuarioEnt entidad = new UsuarioEnt();
            entidad.IdUsuario = long.Parse(q);
            return View(entidad);
        }

        [HttpPost]
        public IActionResult CambiarClaveCuenta(UsuarioEnt entidad)
        {
            var resp = _usuarioModel.CambiarClaveCuenta(entidad);
            if (resp == 1)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.MensajePantalla = "No se logro cambiar su contraseña";
                return View();

            }
        }

    }
}
