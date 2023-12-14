using GymAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Authorization;

namespace GymAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscripcionesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private string _connection;

        public SubscripcionesController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost]
        [Route("AgregarSubscripcion")]
        [Authorize]
        public IActionResult AgregarSubscripcion(SubscripcionEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("sp_procesar_subscripcion", new
                    {
                        IdUsuario = entidad.IdUsuario,
                        IdPaquete = entidad.idPaquete,
                        DescripcionPago = entidad.Descripcion,
                        MontoPago = entidad.MontoPago,
                    }, commandType: CommandType.StoredProcedure);
                    return Ok(datos>1);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("ObtenerSubscripcion")]
        [Authorize]
        public IActionResult ObtenerSubscripcion(int idUsuario) {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.QuerySingleOrDefault<SubscripcionEnt>("sp_ObtenerSubscripcion", new
                    {
                        idUsuario = idUsuario
                    }, commandType: CommandType.StoredProcedure);
                    var response = datos ?? new SubscripcionEnt();
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
