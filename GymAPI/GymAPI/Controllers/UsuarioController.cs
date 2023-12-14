using GymAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using GymAPI.Utils;
using Microsoft.AspNetCore.Authorization;

namespace GymAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : Controller
    {

        private readonly IConfiguration _configuration;
        private string _connection;
        private readonly IUtils _utils;

        public UsuarioController(IConfiguration configuration, IUtils utils)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");
            _utils = utils;
        }

        [HttpPost]
        [Route("InicioSesion")]
        [AllowAnonymous]
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
                    resultado.Token = _utils.GenerarToken(resultado.IdUsuario.ToString(), resultado.IdRol.ToString());
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


        [HttpPost]
        [Route("RegistrarUsuario")]
        [AllowAnonymous]
        public IActionResult RegistrarUsuario(UsuarioEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("RegistrarUsuario", new
                    {
                        entidad.Identificacion,
                        entidad.NombreCompleto,
                        entidad.Telefono,
                        entidad.Correo,
                        entidad.Contrasenna
                    }, commandType: CommandType.StoredProcedure);
                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPatch]
        [Route("AgregarFotoPerfil")]
        [Authorize]
        public IActionResult AgregarFotoPerfil([FromForm] string foto, int id)
        {
            try
            {
                var img = Convert.FromBase64String(foto);
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("AgregarFotoPerfil", new
                    {
                        foto = img,
                        id = id,
                    }, commandType: CommandType.StoredProcedure);
                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("ModificarPerfil")]
        [Authorize]
        public IActionResult ModificarPerfil(UsuarioEnt entidad) {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.QuerySingleOrDefault("ActualizarPerfil", new
                    {
                        nombre = entidad.NombreCompleto,
                        telefono = entidad.Telefono,
                        email = entidad.Correo,
                        id = entidad.IdUsuario
                    }, commandType: CommandType.StoredProcedure);
                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}