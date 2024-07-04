using Newtonsoft.Json.Linq;
using System.Net;

namespace app_products.Exceptions
{
    public class PersonalException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; } = @"text/plain";

        public PersonalException(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
        }

        public PersonalException(HttpStatusCode statusCode, string message) : base(message)
        {
            this.StatusCode = statusCode;
        }

        public PersonalException(HttpStatusCode statusCode, Exception inner) : this(statusCode, inner.ToString()) { }

        public PersonalException(HttpStatusCode statusCode, JObject errorObject) : this(statusCode, errorObject.ToString())
        {
            this.ContentType = @"application/json";
        }
    }
}
