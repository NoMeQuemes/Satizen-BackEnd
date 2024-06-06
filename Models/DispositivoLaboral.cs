using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satizen_Api.Models
{
    public class DispositivoLaboral
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int idCelular {  get; set; }

        public int idPersonal { get; set; }
        [ForeignKey("idPersonal")]
        public virtual Personal Personales { get; set; }

        public string numCelular { get; set; }
        public string observacionCelular { get; set; }
    }
}
