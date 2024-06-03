using System.ComponentModel.DataAnnotations;

namespace Proyec_Satizen_Api.Models.Dto
{
    public class InstitucionUpdateDto
    {
        [Required]
        public int idInstitucion { get; set; }

        [Required]
        [MaxLength(30)]
        public string? nombreInstitucion { get; set; }
        [Required]
        public string? direccionInstitucion { get; set; }
        [Required]
        public string? telefonoInstitucion { get; set; }
        [Required]
        public string? correoInstitucion { get; set; }
       
        public string? celularInstitucion { get; set; }
    }
}
