using Satizen_Api.models;

namespace Satizen_Api.Datos
{
    public static class ContactoStore
    {
        public static List<ContactoDto> contactoList = new List<ContactoDto>
        {
            new ContactoDto{idContacto=1, idPaciente=2, celularPaciente=26455326,
                celularAcompananteP=26452342, estadoContacto="herido de bala"}
        };
    }
}
