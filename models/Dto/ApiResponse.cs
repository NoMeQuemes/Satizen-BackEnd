using System.Collections.Generic;
using System.Net;

namespace Satizen_Api.models.Dto
{
    public class ApiResponse
    {
        public bool IsExitoso { get; set; } = true;
        public object Resultado { get; set; }
        public string Mensaje { get; set; } = "";
        public HttpStatusCode statusCode { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();

        public ApiResponse() { }

        public ApiResponse(object resultado, string mensaje = "", HttpStatusCode statusCode = HttpStatusCode.OK, bool isExitoso = true)
        {
            Resultado = resultado;
            Mensaje = mensaje;
            this.statusCode = statusCode;
            IsExitoso = isExitoso;
        }

        public void SetError(List<string> errorMessages, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            IsExitoso = false;
            ErrorMessages = errorMessages;
            this.statusCode = statusCode;
        }

        public void SetError(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            IsExitoso = false;
            ErrorMessages = new List<string> { errorMessage };
            this.statusCode = statusCode;
        }
    }
}