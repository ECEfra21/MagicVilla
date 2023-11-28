using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_API.Modelos
{
    public class Villa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; protected set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Detalle { get; set; }
        //[Column(TypeName = "decimal(18,4)")]
        public decimal Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        public string ImageUrl { get; set; }
        public string Amenidad { get; set; }
        public DateTime? FUM { get; set; }
        public DateTime FA { get; set; }
        public void SetId(int id) => Id = id;
    }
}
