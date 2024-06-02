using Proyec_Satizen_Api.Models.Dto;

namespace Proyec_Satizen_Api.Datos
{
    public static class InstitucionStore
    {
        public static List<InstitucionDto> institucionList = new List<InstitucionDto>
        {
             new InstitucionDto{idInstitucion=1, nombreInstitucion="Santa Clara", direccionInstitucion="calle pepito", telefonoInstitucion="263546372", correoInstitucion="santaclara@gmail.com", celularInstitucion="263748562"},
                new InstitucionDto{idInstitucion=2, nombreInstitucion="Santa Claroo", direccionInstitucion="calle jamaina", telefonoInstitucion="2783546372", correoInstitucion="santaclar0o@gmail.com", celularInstitucion="187248562"}
        };
    }
}
