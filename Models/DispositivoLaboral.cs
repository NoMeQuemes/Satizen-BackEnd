using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satizen_Api.Models.DispositivoLaboral
{
    public class DispositivoLaboral
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idTelefonoEmpresa { get; set; }

        public int? idPersonal { get; set; }
        [ForeignKey("Personal")]
        public virtual Personal? Personals { get; set; }

        [Required]
        public required string numeroEmpresa { get; set; }

        [Required]
        public  string? marca { get; set; }

        [Required]
        public  string? modelo { get; set; }

        [Required]
        public  string? almacenamiento { get; set; }

        [Required]
        public  string? color { get; set; }
    }
}
