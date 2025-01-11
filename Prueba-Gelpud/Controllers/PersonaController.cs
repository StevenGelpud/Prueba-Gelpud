using Microsoft.AspNetCore.Mvc;
using Prueba_Gelpud.Models;

namespace Prueba_Gelpud.Controllers
{
    public class PersonaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var personas = _context.Personas.ToList();
            return View("_PersonaList", personas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Persona persona)
        {
            try
            {
                // Procesar el campo "Activo" desde el formulario
                if (bool.TryParse(Request.Form["Activo"], out bool activo))
                {
                    persona.Activo = activo;
                }
                else
                {
                    persona.Activo = false; // Valor predeterminado si no se envía
                }

                // Validar el género (aunque el dropdown ya limita las opciones)
                if (persona.Genero != 'M' && persona.Genero != 'F')
                {
                    return BadRequest(new { success = false, message = "El campo Género debe ser 'M' o 'F'." });
                }

                 // Asegurar que se genere un nuevo ID

                if (ModelState.IsValid)
                {
                    _context.Personas.Add(persona);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, persona });
                }

                return BadRequest(new { success = false, message = "Datos no válidos.", errors = ModelState });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Persona persona)
        {
            try
            {
                // Validar si el cuerpo de la solicitud tiene el formato esperado
                if (persona == null)
                {
                    return BadRequest(new { success = false, message = "No se proporcionaron datos válidos." });
                }

                // Procesar el campo "Activo" desde el JSON
                if (persona.Activo == null)
                {
                    persona.Activo = false; // Valor predeterminado si no se envía
                }

                // Validar el género
                if (persona.Genero != 'M' && persona.Genero != 'F')
                {
                    return BadRequest(new { success = false, message = "El campo Género debe ser 'M' o 'F'." });
                }

                // Verificar que el modelo sea válido
                if (ModelState.IsValid)
                {
                    _context.Personas.Update(persona);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, persona });
                }

                return BadRequest(new { success = false, message = "Datos no válidos.", errors = ModelState });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = $"Error: {ex.Message}" });
            }
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var persona = _context.Personas.Find(id);
            if (persona == null)
            {
                return NotFound();
            }

            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();
            return Json(new { success = true, id });
        }
    }
}
