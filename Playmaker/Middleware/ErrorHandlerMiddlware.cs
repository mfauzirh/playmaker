using System.Net;
using System.Text.Json;
using Playmaker.Dtos;
using Playmaker.Exceptions;

namespace Playmaker.Middleware;

public class ErrorHandlerMiddlware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddlware> _logger;

    public ErrorHandlerMiddlware(RequestDelegate next, ILogger<ErrorHandlerMiddlware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ResponseException ex)
        {
            await SendErrorResponse(context, "application/json", ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled execption occurred");
            await SendErrorResponse(context, "application/json", HttpStatusCode.InternalServerError, "Server error occurred.");
        }
    }

    private static async Task SendErrorResponse(HttpContext context, string contentType, HttpStatusCode statusCode, string errorMessage)
    {
        context.Response.ContentType = contentType;
        context.Response.StatusCode = (int)statusCode;

        var errorResponse = new Response<object>
        {
            Data = null,
            Success = false,
            Errors = errorMessage
        };

        var jsonErrorResponse = JsonSerializer.Serialize(errorResponse);

        await context.Response.WriteAsync(jsonErrorResponse);
    }
}