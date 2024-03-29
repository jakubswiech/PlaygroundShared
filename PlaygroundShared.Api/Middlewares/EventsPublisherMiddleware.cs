﻿using Microsoft.AspNetCore.Http;
using PlaygroundShared.Application.Services;

namespace PlaygroundShared.Api.Middlewares;

public class EventsPublisherMiddleware
{
    private readonly RequestDelegate _next;

    public EventsPublisherMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context, IEventsService eventsService)
    {
        try
        {
            await _next(context);
            await eventsService.ExecuteEventsAsync();
        }
        catch(Exception)
        {
            eventsService.Clear();
            throw;
        }
    }
}