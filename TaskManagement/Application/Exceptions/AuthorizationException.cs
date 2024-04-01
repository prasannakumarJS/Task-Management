using Common.Utilities;
using System;
using System.Net;

namespace Application.Exceptions
{
    public class AuthorizationException : Exception
    {
        public HttpStatusCode Status { get; }

        public override string Message { get; }

        public AuthorizationException(HttpStatusCode statusCode)
        {
            this.Status = statusCode;
            this.Message = HttpStatusCodeHelper.GetStatusErrorMessage(statusCode);
        }
    }
}