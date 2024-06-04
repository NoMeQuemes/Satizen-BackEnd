using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satizen_Api.Models
{
    public class Sectores
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int idSector { get; set; }

        public string nombreSector { get; set; }
        public string descripcionSector { get; set; }

    }
}
