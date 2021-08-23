using Microsoft.EntityFrameworkCore;


namespace BackEnd.Models
{
    public class AmbulantaContext : DbContext
    {
        public DbSet<Gradjanin> Gradjani {get; set;}
        public DbSet<Vakcina> Vakcine {get; set;}

        public DbSet<Ambulanta> Ambulante {get; set;}

        public AmbulantaContext(DbContextOptions options) : base(options)
        {

        }
    }
}