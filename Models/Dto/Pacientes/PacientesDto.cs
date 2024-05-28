﻿using Satizen_Api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satizen_Api.models.Dto
{
    public class PacientesDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idPaciente { get; set; }
        public int idUsuario { get; set; }
        public int idInstitucion { get; set; }
        public string? nombrePaciente { get; set; }
        public int numeroHabitacionPaciente { get; set; }
        public DateTime fechaIngreso { get; set; }
        public DateTime? estadoPaciente { get; set; }
        public string? observacionPaciente { get; set; }

    }
}