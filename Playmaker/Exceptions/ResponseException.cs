using System.Net;

namespace Playmaker.Exceptions;

public class ResponseException : Exception
{
    public HttpStatusCode StatusCode { get; set; }

    public ResponseException(HttpStatusCode statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}