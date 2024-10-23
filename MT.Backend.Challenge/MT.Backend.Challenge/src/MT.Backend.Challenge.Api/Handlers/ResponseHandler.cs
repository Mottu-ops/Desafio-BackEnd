using Microsoft.AspNetCore.Mvc;
using MT.Backend.Challenge.Api.Controllers;
using MT.Backend.Challenge.Domain.Constants;
using MT.Backend.Challenge.Domain.Entities.Response;
using System.Net;

namespace MT.Backend.Challenge.Api.Handlers
{
    public static class ResponseHandler
    {
        public static ActionResult HandleException(ILogger logger, Exception exception)
        {
            logger.LogError(exception, "An error occurred.");
            var errorResponse = new
            {
                Message = ServiceConstants.DefaultErrorMessage,
                Details = exception.Message
            };
            return new ObjectResult(errorResponse) { StatusCode = (int)HttpStatusCode.InternalServerError };
        }

        public static ActionResult HandleResponse<T>(T response)
        {
            if (response == null)
            {
                return new ObjectResult
                    (new { Message = ServiceConstants.NotFoundMessage })
                { StatusCode = (int)HttpStatusCode.NotFound };
            }

            var statusCode = (response as BaseResponse).StatusCode;

            return statusCode switch
            {
                (int)HttpStatusCode.Created => new ObjectResult(response) { StatusCode = (int)HttpStatusCode.Created },
                (int)HttpStatusCode.NotFound => new ObjectResult(response) { StatusCode = (int)HttpStatusCode.NotFound },
                (int)HttpStatusCode.BadRequest => new ObjectResult(response) { StatusCode = (int)HttpStatusCode.BadRequest },
                (int)HttpStatusCode.InternalServerError => new ObjectResult(response) 
                    { StatusCode = (int)HttpStatusCode.InternalServerError },
                _ => new ObjectResult(response) { StatusCode = (int)HttpStatusCode.OK }

            };

        }
    }
}
