using GymAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace GymAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : Controller
    {

        private readonly IConfiguration _configuration;
        private string _connection;

        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");
        }




        [HttpGet]
        [Route("ConsultarUsuarios")]
        public IActionResult ConsultarUsuarios()
        {
            var resultado = new List<UsuarioEnt>();
            var respuesta = new UsuarioEntRespuesta();

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    resultado = connection.Query<UsuarioEnt>("ConsultarUsuarios",
                        new { },
                        commandType: System.Data.CommandType.StoredProcedure).ToList();

                    if (resultado.Count == 0)
                    {
                        respuesta.Codigo = 2;
                        respuesta.Mensaje = "No se encontró la información de los usuarios";
                        return Ok(respuesta);
                    }

                    respuesta.Codigo = 1;
                    respuesta.Mensaje = "Información consultada correctamente";
                    respuesta.Objetos = resultado;
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                respuesta.Codigo = 3;
                respuesta.Mensaje = "Se presentó un incoveninete.";
                respuesta.Objetos = resultado;
                return Ok(respuesta);
            }
        }



        [HttpGet]
        [Route("ConsultarUsuario")]
        [AllowAnonymous]
        public IActionResult ConsultarUsuario(long q)
        {
            var resultado = new UsuarioEnt();
            var respuesta = new UsuarioEntRespuesta();
            long IdUsuario = q;

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    resultado = connection.Query<UsuarioEnt>("ObtenerUsuario",
                        new { IdUsuario },
                        commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                    if (resultado == null)
                    {
                        respuesta.Codigo = 2;
                        respuesta.Mensaje = "No se encontró la información del usuario";
                        return Ok(respuesta);
                    }

                    respuesta.Codigo = 1;
                    respuesta.Mensaje = "Información consultada correctamente";
                    respuesta.Objeto = resultado;
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                respuesta.Codigo = 3;
                respuesta.Mensaje = "Se presentó un incoveninete.";
                respuesta.Objeto = resultado;
                return Ok(respuesta);
            }
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


        [HttpPost]
        [Route("RegistrarUsuario")]

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

        [HttpPut]
        [Route("ModificarUsuario")]
        public IActionResult ModificarUsuario(UsuarioEnt entidad)
        {
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

        [HttpPut]
        [Route("ActualizarEstadoUsuario")]
        //public IActionResult ActualizarEstadoUsuario( int IdUsuario, bool Estado)

        public IActionResult ActualizarEstadoUsuario(UsuarioEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var filasAfectadas = context.Execute("ActualizarEstadoUsuario", new
                    {
                        IdUsuario = entidad.IdUsuario,
                        Estado = entidad.Estado
                    }, commandType: CommandType.StoredProcedure);

                    
                    if (filasAfectadas > 0)
                    {
                        return Ok(1); 
                    }
                    else
                    {
                        return Ok(0); 
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }








    }
}