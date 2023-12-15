using MagicVilla_API.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Datos
{
    public class AplicationDBContext : DbContext
    {
        public DbSet<Villa> Villas { get; set; }
        public DbSet<NumeroVilla> NumeroVilla{ get; set; }
        public AplicationDBContext(DbContextOptions<AplicationDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
            new Villa()
            {
                Id = 1,
                Nombre = "En ningun Lugar",
                Detalle = "Ningun lugar",
                Tarifa = Convert.ToDecimal(1001.21),
                Ocupantes = 100,
                MetrosCuadrados = 1000,
                ImageUrl = "",
                Amenidad = "ameno",
                FUM = null,
                FA = DateTime.Now
            },
            new Villa()
            {
                Id = 2,
                Nombre = "En ningun Lugar",
                Detalle = "Ningun lugar",
                Tarifa = Convert.ToDecimal(1001.21),
                Ocupantes = 100,
                MetrosCuadrados = 1000,
                ImageUrl = "",
                Amenidad = "ameno",
                FUM = null,
                FA = DateTime.Now
            });
        }

    }
}