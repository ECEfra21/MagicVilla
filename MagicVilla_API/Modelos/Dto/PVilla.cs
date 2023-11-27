using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Modelos.Dto
{
    public class PVilla
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        [Required]
        public decimal Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        public string ImageUrl { get; set; }
        public string Amenidad { get; set; }
        public DateTime? FUM { get; set; }
        public DateTime? FA { get; set; }
    }
}
