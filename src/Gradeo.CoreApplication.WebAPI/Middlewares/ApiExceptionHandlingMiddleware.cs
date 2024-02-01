using System.Text.RegularExpressions;
using Gradeo.CoreApplication.Application.Common.DTOs;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace Gradeo.CoreApplication.WebAPI.Middlewares;

public static class ApiExceptionHandlingMiddleware
{
    public static IApplicationBuilder UseApiExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (context != null)
                {
                    var error = new Error()
                    {
                        Message = GetErrorMessageFromDetails(contextFeature.Error.ToString()),
                        StatusCode = StatusCodes.Status500InternalServerError,
                        ErrorDetails = contextFeature.Error.ToString()
                    };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
                }
            });
        });
    }

    private static string GetErrorMessageFromDetails(string errorDetails)
    {
        var errorRegex = new Regex(@"([\w.]*:\s)([\w\s]*)([\r\n])(\|*)");
        var matching = errorRegex.Match(errorDetails);
        
        return matching.Groups.Count > 1 ? matching.Groups[2].Value.TrimEnd() : errorDetails;
    }
}