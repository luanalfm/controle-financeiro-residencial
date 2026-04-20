using System.Net;
using System.Text.Json;
using ControleGastos.Application.Exceptions;
using ControleGastos.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ControleGastos.Api.Middleware;

/// <summary>
/// Centraliza os erros em ProblemDetails JSON para respostas previsíveis
/// Basicamente sendo um tratador global de exceções que captura qualquer erro que aconteça na aplicação e retorna uma resposta padronizada em JSON
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (NotFoundException ex)
        {
            await WriteProblemAsync(context, HttpStatusCode.NotFound, ex.Message);
        }
        catch (DomainException ex)
        {
            await WriteProblemAsync(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro Desconhecido.");
            var message = context.RequestServices.GetRequiredService<IHostEnvironment>().IsDevelopment()
                ? ex.Message
                : "Ocorreu um erro interno. Tente novamente mais tarde.";
            await WriteProblemAsync(context, HttpStatusCode.InternalServerError, message);
        }
    }

    private static Task WriteProblemAsync(HttpContext context, HttpStatusCode statusCode, string detail)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)statusCode;

        var problem = new
        {
            type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            title = statusCode.ToString(),
            status = (int)statusCode,
            detail
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(problem, JsonOptions));
    }
}
