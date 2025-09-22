using Azure;
using DomainLayer.Exceptions;
using Shared.ErrorModels;
using System.Text.Json;

namespace E_Commerc.web.MiddleWares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
        public CustomExceptionHandlerMiddleware(RequestDelegate Next,ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next=Next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
                await HandleNotFoundEndPointAsync(httpContext);
                
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Somthing Went Wrong");
                await HandleExceptionAsync(httpContext, ex);

            }

        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            //create object of ErrorToReturn to send it as response 
            var Response = new ErrorToReturn()
            {
               
                ErrorMessage = ex.Message
            };
            Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException=>StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException => GetBadRequestErrors(badRequestException, Response),
                _ => StatusCodes.Status500InternalServerError
            };
            httpContext.Response.StatusCode = Response.StatusCode;
            //object(Response) dlw2ty C# code f return it as json
            await httpContext.Response.WriteAsJsonAsync(Response);
        }


        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"EndPoint {httpContext.Request.Path} is Not Found"
                };

                await httpContext.Response.WriteAsJsonAsync(Response);
            }
        }
        // to extract errors from BadRequestException and set them in response object
        private static int GetBadRequestErrors(BadRequestException badRequestException, ErrorToReturn response)
        {
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;

        }
    }
}
