using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Satizen_Api.Models
{
    public class Personal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idPersonal { get; set; }
        public int? idInstitucion { get; set; }
        [ForeignKey("idInstitucion")]
        public virtual Institucion? institucion { get; set; }
        [Required]
        public string? nombrePersonal { get; set; }
        [Required]
        public string? rolPersonal { get; set; }
        [Required]
        public int celularPersonal { get; set; }
        [Required]
        public int telefonoPersonal { get; set; }
        [Required]
        public string? correoPersonal { get; set; }
    }
}
