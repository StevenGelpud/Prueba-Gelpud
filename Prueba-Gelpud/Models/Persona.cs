using System;
using System.ComponentModel.DataAnnotations;

namespace Prueba_Gelpud.Models
{
    public class Persona
    {
        [Key]
        public int? IdPersona { get; set; }

        [Required]
        public string Nombres { get; set; }

        [Required]
        public string Apellidos { get; set; }

        [Required]
        public string Identificacion { get; set; }

        [Required]
        public char Genero { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public string Contraseña { get; set; }

        [Required]
        public bool Activo { get; set; }
    }
}
