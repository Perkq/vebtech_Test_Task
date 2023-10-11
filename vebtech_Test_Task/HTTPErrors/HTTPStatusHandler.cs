using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using vebtech_Test_Task.Exceptions;

namespace vebtech_Test_Task.HTTPErrors
{
    public class HTTPStatusHandler
    {
        private readonly RequestDelegate next;

        public HTTPStatusHandler(RequestDelegate _next)
        {
            next = _next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    NotFoundException => (int)HttpStatusCode.NotFound,
                    KeyNotFoundException e =>
                        (int)HttpStatusCode.NotFound,
                    ArgumentException e =>
                        (int)HttpStatusCode.BadRequest,
                    InvalidCredentialException e =>
                        (int)HttpStatusCode.Unauthorized,
                    InvalidParametresDataException e =>
                        (int)HttpStatusCode.Conflict, 
                    InvalidOperationException e =>
                        (int)HttpStatusCode.NotFound,
                        _ => (int)HttpStatusCode.InternalServerError
                };

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
