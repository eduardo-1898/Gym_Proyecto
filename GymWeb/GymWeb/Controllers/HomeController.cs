using GymWeb.Entities;
using GymWeb.Interfaces;
using GymWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GymWeb.Controllers
{
    public class HomeController : Controller
    {




        //REGISTRO DE BITACORA
        //private void RegistrarEnBitacora(string descripcion, string usuario, string modulo, int idTipoBitacora)
        //{

        //    ViewBag.Mensaje = string.Empty;
        //    var bitacora = new BitacoraEnt
        //    {
        //        BI_DESCRIPCION = descripcion,
        //        USUARIO = usuario,
        //        MODULO = modulo,
        //        REF_ID_TIPO_BITACORA = idTipoBitacora
        //    };
        //    _bitacoraModel.InsertarBitacora(bitacora);
        //}
        ////REGISTRO DE BITACORA



        private readonly IUsuarioModel _usuarioModel;
       // private readonly IBitacoraModel _bitacoraModel;

        public HomeController(IUsuarioModel usuarioModel)
        {
            //, IBitacoraModel? bitacoraModel
            _usuarioModel = usuarioModel;
            //_bitacoraModel = bitacoraModel;
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






        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}