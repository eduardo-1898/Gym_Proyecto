using GymAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private IHostEnvironment _hostingEnvironment;

        public EjercicioController(IConfiguration configuration, IHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");
            _hostingEnvironment = hostingEnvironment;
        }
           

        [HttpPost]
        [Route("CrearEjercicio")]
        public IActionResult CrearEjercicio(EjercicioEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("CrearEjercicio", new
                    {
                        entidad.NombreEjercicio,
                        entidad.DescripcionEjercicio,
                        entidad.VideoEjercicio
                    }, commandType: CommandType.StoredProcedure);
                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                using (var context = new SqlConnection(_connection))
                {
                    try
                    {
                        var datos = context.Execute("InsertarBitacoraErrores", new
                        {
                            Descripcion = ex.Message,
                            Tipo = "Api"
                        }, commandType: CommandType.StoredProcedure);
                        return BadRequest(ex.Message);
                    }
                    catch (Exception innerEx)
                    {
                        // En caso de que falle la inserción en la bitácora, puedes registrar el error en algún otro lugar o manejarlo adecuadamente.
                        return BadRequest($"Error al registrar en la bitácora: {innerEx.Message}");
                    }
                }
            }
        }




        [HttpGet]
        [Route("ConsultarEjercicio")]
        public IActionResult ConsultarEjercicio()
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<EjercicioEnt>("ConsultarEjercicio",
                        new { },
                        commandType: CommandType.StoredProcedure).ToList();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                using (var context = new SqlConnection(_connection))
                {
                    try
                    {
                        var datos = context.Execute("InsertarBitacoraErrores", new
                        {
                            Descripcion = ex.Message,
                            Tipo = "Api"
                        }, commandType: CommandType.StoredProcedure);
                        return BadRequest(ex.Message);
                    }
                    catch (Exception innerEx)
                    {
                        // En caso de que falle la inserción en la bitácora, puedes registrar el error en algún otro lugar o manejarlo adecuadamente.
                        return BadRequest($"Error al Consultar Ejercicio en la bitácora: {innerEx.Message}");
                    }
                }
            }
        }

        //Fin del metodo

        [HttpDelete]
        [Route("EliminarEjercicio")]
        public IActionResult EliminarEjercicio(long q)
        {
            try
            {
                long IdEjercicio = q;

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("EliminarEjercicio",
                        new { IdEjercicio },
                        commandType: CommandType.StoredProcedure);

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                using (var context = new SqlConnection(_connection))
                {
                    try
                    {
                        var datos = context.Execute("InsertarBitacoraErrores", new
                        {
                            Descripcion = ex.Message,
                            Tipo = "Api"
                        }, commandType: CommandType.StoredProcedure);
                        return BadRequest(ex.Message);
                    }
                    catch (Exception innerEx)
                    {
                        // En caso de que falle la inserción en la bitácora, puedes registrar el error en algún otro lugar o manejarlo adecuadamente.
                        return BadRequest($"Error al Eliminar Ejercicio en la bitácora: {innerEx.Message}");
                    }
                }
            }
        }

        //Fin del metodo



    }
}
