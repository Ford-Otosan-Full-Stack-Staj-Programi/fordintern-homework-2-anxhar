﻿namespace OdevApi.Web.Middleware;

public class HeartbeatMiddleware
{
    private readonly RequestDelegate _next;

    public HeartbeatMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/heartbeat"))
        {
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync("Welcome Server");
            return;
        }

        await _next.Invoke(context);
    }
}
