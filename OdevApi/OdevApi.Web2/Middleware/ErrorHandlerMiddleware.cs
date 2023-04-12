using OdevApi.Base;
using Serilog;
using System.Net;
using System.Text.Json;

namespace OdevApi.Web.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var messageError = string.Empty;

            switch (error)
            {
                case MessageResultException ex:
                    messageError = ex.Message;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    messageError = "Internal Server Error";
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            Log.Error(error, messageError);

            var result = JsonSerializer.Serialize(messageError);
            await response.WriteAsync(result);
        }
    }
}