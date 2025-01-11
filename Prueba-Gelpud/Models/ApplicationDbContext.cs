using Microsoft.EntityFrameworkCore;

namespace Prueba_Gelpud.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Persona> Personas { get; set; }
    }
}
