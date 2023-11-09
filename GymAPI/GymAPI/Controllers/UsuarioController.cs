using GymAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Net.Mail;
using System.Text;

namespace GymAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : Controller
    {

        private readonly IConfiguration _configuration;
        private string _connection;
        private IHostEnvironment _hostingEnvironment;

        public UsuarioController(IConfiguration configuration, IHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");
            _hostingEnvironment = hostingEnvironment;
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


        [HttpPost]
        [Route("RecuperarCuenta")]
        public IActionResult RecuperarCuenta(UsuarioEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<UsuarioEnt>("RecuperarContrasenna",
                        new { entidad.Correo },
                        commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (datos != null)
                    {

                        string contrasennaTemporal = GenerarCodigo();
                        string contenido = ArmarHTML(datos, contrasennaTemporal);

                        context.Execute("ActualizarClaveTemporal",
                            new { datos.IdUsuario, contrasennaTemporal },
                            commandType: CommandType.StoredProcedure);

                        EnviarCorreo(datos.Correo, "Restaurar Contraseña", contenido);
                        return Ok(1);
                    }
                    return Ok(0);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut]
        [Route("CambiarClaveCuenta")]

        public IActionResult CambiarClaveCuenta(UsuarioEnt entidad)
        {
            try
            {

                entidad.Estado = true;

                using (var context = new SqlConnection(_connection))

                {
                    var datos = context.Execute("CambiarClaveCuenta", new
                    {
                        entidad.IdUsuario,
                        entidad.Contrasenna,
                        entidad.contrasennaTemporal
                    }, commandType: CommandType.StoredProcedure);
                    return Ok(datos);

                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        private string ArmarHTML(UsuarioEnt datos, string contrasennaTemporal)
        {
            string rutaArchivo = Path.Combine(_hostingEnvironment.ContentRootPath, "CorreosTemplate\\Contrasenna.html");
            string htmlArchivo = System.IO.File.ReadAllText(rutaArchivo);
            htmlArchivo = htmlArchivo.Replace("@@Nombre", datos.NombreCompleto);
            htmlArchivo = htmlArchivo.Replace("@@ClaveTemporal", contrasennaTemporal);
            htmlArchivo = htmlArchivo.Replace("@@Link", "https://localhost:7214/Login/CambiarClaveCuenta?q=" + datos.IdUsuario.ToString());


            return htmlArchivo;


        }


        private void EnviarCorreo(string Destinatario, string Asunto, string Mensaje)
        {
            string correoSMTP = _configuration.GetSection("Llaves:correoSMTP").Value;
            string claveSMTP = _configuration.GetSection("Llaves:claveSMTP").Value;

            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(Destinatario));
            msg.From = new MailAddress(correoSMTP);
            msg.Subject = Asunto;
            msg.Body = Mensaje;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(correoSMTP, claveSMTP);
            client.Port = 587;
            client.Host = "smtp.office365.com";
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Send(msg);
        }

        private string GenerarCodigo()
        {
            int length = 4;
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }


    }
}