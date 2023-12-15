using System.Net;

namespace MagicVilla_API.Modelos
{
    public class ApiResponse
    {
        public bool Exitoso { get; set; } = true;
        public List<string> Erores { get; set; }
        public object Objeto { get; set; } = null;
        public HttpStatusCode Estado { get; set; } = HttpStatusCode.BadRequest;
    }
}
