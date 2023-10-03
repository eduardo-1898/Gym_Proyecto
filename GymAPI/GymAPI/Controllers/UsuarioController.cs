using GymAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Authorization;



namespace GymAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : Controller
    {

        private readonly IConfiguration _configuration;

        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }





        [HttpPost]
        [Route("InicioSesion")]
        public IActionResult InicioSesion(UsuarioEnt entidad)
        {
            var resultado = new UsuarioEnt();
            var respuesta = new UsuarioEntRespuesta();




            try
            {

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {

                    resultado = connection.Query<UsuarioEnt>("ConsultarUsuario",

                        new { entidad.Correo, entidad.Contrasenna },
                        commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                    if (resultado == null)
                    {
                        respuesta.Codigo = 2;
                        respuesta.Mensaje = "No se encontró la información de su usuario";
                        return Ok(respuesta);
                    }

                    respuesta.Codigo = 1;
                    respuesta.Mensaje = "Su usuario fue validado correctamente";
                    respuesta.Objeto = resultado;
                    return Ok(respuesta);
                }


            }
            catch (Exception)
            {

                respuesta.Codigo = 3;
                respuesta.Mensaje = "Se presentó un inconveniente";
                return Ok(respuesta);


            }
        }




    }
}
