using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace TeknikServis.WebAPI.Middlewares;

public class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";

        if (exception is ValidationException vEx)
        {
            httpContext.Response.StatusCode = 400;
            var msgs = vEx.Errors.Select(e => e.ErrorMessage).ToList();
            var errorResult = Result<string>.Failure(400, msgs);
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorResult), cancellationToken);
            return true;
        }

        httpContext.Response.StatusCode = 500;
        var list = new List<string> { exception.Message };
        var inner = exception.InnerException;
        while (inner != null)
        {
            list.Add(inner.Message);
            inner = inner.InnerException;
        }
        if (exception is DbUpdateException && !list.Any(m => m.Contains("DbUpdateException", StringComparison.OrdinalIgnoreCase)))
        {
            list.Insert(0, "Database update error (check FK constraints, NOT NULL, conversions).");
        }

        var result = Result<string>.Failure(500, list);
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(result), cancellationToken);
        return true;
    }
}
