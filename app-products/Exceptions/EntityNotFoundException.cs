using System.Net;

namespace app_products.Exceptions
{
    public class EntityNotFoundException : PersonalException
    {
        private static readonly string _message = "No se encontro";

        public EntityNotFoundException() : base(HttpStatusCode.BadRequest, _message)
        {
        }
        public EntityNotFoundException(string message) : base(HttpStatusCode.BadRequest, message)
        {
        }
    }


}
