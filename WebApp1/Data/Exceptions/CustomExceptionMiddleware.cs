using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebApp1.Data.ViewModel;

namespace WebApp1.Data.Exceptions
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {

            httpContext.Response.StatusCode = (int)ExceptionStatuscode.GetExceptionStatuscode(ex.GetType());

            httpContext.Response.ContentType = "application/json";

            var response = new ErrorVM()
            {
                CorelationalId = Guid.NewGuid().ToString(),
                Errors = new List<Errors>()
                {
                    new Errors(){
                    Source = ex.Source,
                    Description = ex.Message
                    }
                }
            };

            return httpContext.Response.WriteAsync(response.ToString());
        }
    }
}
