using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satizen_Api.Models
{
    public class Personal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idPersonal { get; set; }

        public int idInstitucion { get; set; }
        public string? nombrePersonal { get; set; }
        public string? rolPersonal { get; set; }
        public int celularPersonal { get; set; }
        public int telefonoPersonal { get; set; }
        public string? correoPersonal { get; set; }
    }

}
