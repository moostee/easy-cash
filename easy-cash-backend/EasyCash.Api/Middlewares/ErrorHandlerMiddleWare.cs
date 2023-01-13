
using System.Net;
using EasyCash.Shared;
using System.Text.Json;
using EasyCash.Shared.Exceptions;
using EasyCash.Application.Exceptions;

namespace EasyCash.Middlewares
{
    internal class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _env;
        public ErrorHandlerMiddleware(RequestDelegate next,
            ILogger<ErrorHandlerMiddleware> logger,
            IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (EasyCashException error)
            {
                if (error.ValidationErrors != null) error.StatusMessage = error.FormattedError;

                //_logger.LogError(error, error.Message);
                var response = context.Response;
                response.ContentType = "application/json";

                Result<string> serviceResponse = new()
                {
                    StatusCode = error.StatusCode,
                    StatusMessage = error.StatusMessage,

                };

                response.StatusCode = (int)HttpStatusCode.BadRequest;
                var result = JsonSerializer.Serialize(serviceResponse);

                await response.WriteAsync(result);

            }
            catch (DuplicateEntryException error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                Result<string> serviceResponse = new()
                {
                    StatusCode = EasyCash.Shared.StatusCodes.INVALID_REQUEST,
                    StatusMessage = error.Message,

                };

                response.StatusCode = (int)HttpStatusCode.BadRequest;
                var result = JsonSerializer.Serialize(serviceResponse);

                await response.WriteAsync(result);
            }
            catch (NotFoundException error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                Result<string> serviceResponse = new()
                {
                    StatusCode = EasyCash.Shared.StatusCodes.INVALID_REQUEST,
                    StatusMessage = error.Message,

                };

                response.StatusCode = (int)HttpStatusCode.NotFound;
                var result = JsonSerializer.Serialize(serviceResponse);

                await response.WriteAsync(result);
            }
            catch (Exception error)
            {
                //_logger.LogError(error, error.Message);
                var response = context.Response;
                response.ContentType = "application/json";

                Result<string> serviceResponse = new()
                {
                    StatusCode = EasyCash.Shared.StatusCodes.INVALID_REQUEST,
                };


                serviceResponse.StatusMessage = error.Message;


                // unhandled error
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var result = JsonSerializer.Serialize(serviceResponse);

                await response.WriteAsync(result);
            }
        }
    }
}
