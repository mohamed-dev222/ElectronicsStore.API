using System.Text.Json;
using ElectronicsStore.API.Exceptions;
using ElectronicsStore.API.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace ElectronicsStore.API.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Unhandled exception");
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            var errorCode = exception switch
            {
                NotFoundException => "RESOURCE_NOT_FOUND",
                ValidationException => "VALIDATION_ERROR",
                InsufficientStockException => "INSUFFICIENT_STOCK",
                _ => "INTERNAL_SERVER_ERROR"
            };

            var message = exception.Message;
            if (!_environment.IsDevelopment() && context.Response.StatusCode == StatusCodes.Status500InternalServerError)
            {
                message = "An error occurred while processing your request.";
            }

            var response = ApiResponse<object>.Fail(message, new { ErrorCode = errorCode, StackTrace = _environment.IsDevelopment() ? exception.StackTrace : null });

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}