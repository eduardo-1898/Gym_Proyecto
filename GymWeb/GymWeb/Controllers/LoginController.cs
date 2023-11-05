using Microsoft.AspNetCore.Mvc;
using GymWeb.Models;
using GymWeb.Entities;
using Newtonsoft.Json;

namespace GymWeb.Controllers
{
    public class LoginController : Controller
    {

        private readonly ILogger<LoginController> _logger;
        private readonly IUsuarioModel _usuarioModel;
        private readonly ISubscripcionModel _subscripcionModel;

        public LoginController(ILogger<LoginController> logger, IUsuarioModel usuarioModel, ISubscripcionModel subscripcionModel)
        {
            _logger = logger;
            _usuarioModel = usuarioModel;
            _subscripcionModel = subscripcionModel;
        }

        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpGet]
        public IActionResult RecuperarContrasenna()
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
                    return View("Login");
                }

                var subs = _subscripcionModel.getSubscription(datos.Objeto.IdUsuario);

                HttpContext.Session.SetString("RolUsuario", datos.Objeto.NombreRol??"");
                HttpContext.Session.SetString("userInfo",JsonConvert.SerializeObject(datos.Objeto));
                HttpContext.Session.SetString("subscripcion",JsonConvert.SerializeObject(subs));

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Consulte con el administrador";
                return View("Login");
            }
        }

        public IActionResult Index()
        {
            return View();
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
