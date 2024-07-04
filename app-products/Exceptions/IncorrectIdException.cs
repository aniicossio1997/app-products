using System;
using System.Net;

namespace app_products.Exceptions
{
    public class IncorrectIdException : PersonalException
    {
        private static readonly string message = "Los Ids no coiciden";

        public IncorrectIdException() : base(HttpStatusCode.BadRequest, message)
        {
        }
    }
}
