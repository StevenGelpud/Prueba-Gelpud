using Microsoft.AspNetCore.Mvc;
using Prueba_Gelpud.Models;

namespace Prueba_Gelpud.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para probar la conexión a la base de datos
        [HttpGet("dbconnection")]
        public IActionResult TestDatabaseConnection()
        {
            try
            {
                // Verifica si se puede conectar a la base de datos
                var canConnect = _context.Database.CanConnect();
                if (canConnect)
                {
                    return Ok(new { success = true, message = "Conexión exitosa a la base de datos." });
                }
                return BadRequest(new { success = false, message = "No se pudo conectar a la base de datos." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = $"Error al probar la conexión: {ex.Message}" });
            }
        }

        // Método para insertar datos en la base de datos
        [HttpPost("addperson")]
        public IActionResult AddPerson([FromBody] Persona persona)
        {
            try
            {
                // Verificar si el objeto recibido es válido
                if (persona == null || !ModelState.IsValid)
                {
                    return BadRequest(new { success = false, message = "Datos inválidos." });
                }

                // Verificar conexión a la base de datos antes de intentar guardar
                if (!_context.Database.CanConnect())
                {
                    return BadRequest(new { success = false, message = "No se pudo conectar a la base de datos." });
                }

                // Guardar la persona en la base de datos
                _context.Personas.Add(persona);
                _context.SaveChanges();

                // Confirmar el registro exitoso
                return Ok(new { success = true, message = "Persona registrada con éxito.", data = persona });
            }
            catch (Exception ex)
            {
                // Manejar errores
                return BadRequest(new { success = false, message = $"Error al registrar la persona: {ex.Message}" });
            }
        }
    }
}

