using GymAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntrenadorController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private string _connection;
        private IHostEnvironment _hostingEnvironment;

        public EntrenadorController(IConfiguration configuration, IHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");
            _hostingEnvironment = hostingEnvironment;
        }

        //Fin del constructor
        [HttpPost]
        [Route("CrearCita")]

        public IActionResult CrearCita(EntrenadorEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("CrearCita", new
                    {
                        entidad.FechaCita,
                        entidad.EspaciosDisponibles                        
                    }, commandType: CommandType.StoredProcedure);
                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        //Fin del metodo


        [HttpGet]      
        [Route("EditarCitas")]
        public IActionResult EditarCitas()
        {
            try
            {          
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<EntrenadorEnt>("EditarCitas",
                        new {  },
                        commandType: CommandType.StoredProcedure).ToList();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Fin del metodo

        [HttpDelete]      
        [Route("EliminarCita")]
        public IActionResult EliminarCita(long q)
        {
            try
            {
                long IdCita = q;

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("EliminarCita",
                        new { IdCita },
                        commandType: CommandType.StoredProcedure);

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Fin del metodo



    }
}

