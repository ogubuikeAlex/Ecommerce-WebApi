using System.Net;
using KingsStoreApi.Model.ErrorModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace KingsStoreApi.Extensions
{
    public static class ExceptionMiddleWareException
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature is not null)
                    { 
                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            Message = contextFeature.Error.Message,
                            StatusCode = context.Response.StatusCode
                        }.ToString());
                    }
                });
            });
        }
    }
}