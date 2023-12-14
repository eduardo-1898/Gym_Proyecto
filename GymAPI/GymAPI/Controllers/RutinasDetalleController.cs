using GymAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace GymAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RutinasDetalleController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private string _connection;

        public RutinasDetalleController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");
        }


        [HttpPost]
        [Route("AgregarRutinas")]
        [Authorize]
        public IActionResult AgregarRutinas(RutinasDetalleEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("sp_AgregarRutinasDetalle", new
                    {
                        idRutina = entidad.idRutina,
                        idEjercicio = entidad.idEjercicio,
                    }, commandType: CommandType.StoredProcedure);
                    return Ok(datos > 0);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("ObtenerRutinas")]
        [Authorize]
        public IActionResult ObtenerRutinas()
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<RutinasDetalleEnt>("SELECT * FROM RutinasDetalle", commandType: CommandType.Text).ToList();
                    var response = datos ?? new List<RutinasDetalleEnt>();
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("EliminarRutina")]
        [Authorize]
        public IActionResult EliminarRutina(int id)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<RutinasDetalleEnt>("DELETE FROM RutinasDetalle WHERE Id = @id", new { id= id }, commandType: CommandType.Text).ToList();
                    var response = datos ?? new List<RutinasDetalleEnt>();
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
