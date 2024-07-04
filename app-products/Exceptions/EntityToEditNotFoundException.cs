using System.Net;

namespace app_products.Exceptions
{
    public class EntityToEditNotFoundException:PersonalException
    {
        private static readonly string message = "No se encontro la entidad";

        public EntityToEditNotFoundException() : base(HttpStatusCode.BadRequest, message)
        {

        }
    }
}
