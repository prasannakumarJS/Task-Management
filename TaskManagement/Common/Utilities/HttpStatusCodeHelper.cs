using System.Net;

namespace Common.Utilities
{
    public static class HttpStatusCodeHelper
    {
        public static string GetStatusErrorMessage(HttpStatusCode statusCode)
            => statusCode switch
            {
                HttpStatusCode.Unauthorized => "Unauthorize due to incorrect policy.",
                _ => "Unauthorized"
            };
    }
}
