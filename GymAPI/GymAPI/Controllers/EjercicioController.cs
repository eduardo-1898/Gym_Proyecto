using GymAPI.Entities;
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
    public class EjercicioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private string _connection;

        public EjercicioController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");
        }


        [HttpPost]
        [Route("AgregarEjercicio")]
        public IActionResult AgregarEjercicio(EjercicioEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("InsertarEjercicio", new
                    {
                        Nombre=entidad.Nombre,
                    }, commandType: CommandType.StoredProcedure);
                    return Ok(datos > 1);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult ObtenerEjercicios()
        {
            try
            {
                using (var connection = new SqlConnection(_connection))
                {
                    connection.Open();

                    var ejercicios = connection.Query<EjercicioEnt>("ObtenerEjercicios", commandType: CommandType.StoredProcedure);

                    return Ok(ejercicios);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
