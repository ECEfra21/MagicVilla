using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Modelos.Dto
{
    public class PNumeroVilla
    {
        [Required]
        public int Numero { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string Detalle { get; set; }        
    }
}
