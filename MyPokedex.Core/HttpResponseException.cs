namespace MyPokedex.Core
{
    using System;
    using System.Net;

    public class HttpResponseException : Exception
    {
        public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;

        public object Value { get; set; }
    }
}
