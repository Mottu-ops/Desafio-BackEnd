using MT.Backend.Challenge.Domain.Entities.Response;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MT.Backend.Challenge.Domain.Entities;
using MT.Backend.Challenge.Domain.Constants;

namespace MT.Backend.Challenge.Application.Helpers
{
    public class HandleResponseExceptionHelper
    {
        private readonly ILogger Logger;

        public HandleResponseExceptionHelper(ILogger logger)
        {
            Logger = logger;
        }

        public void HandleResponseException<T>(BaseResponse response, OperationResult<T> operationResult)
        {
            response.Message = ServiceConstants.DefaultErrorMessage;

            if (operationResult.Exception != null)
            {
                HandleException(response, operationResult.Exception);
            }
            else
            {
                Logger.LogError(operationResult.Message);
                response.StatusCode = 400;
            }
        }

        public void HandleException(BaseResponse response, Exception ex)
        {
            response.Message = ServiceConstants.DefaultErrorMessage;
            response.StatusCode = 500;
            Logger.LogError(ex, response.Message);
        }

    }
}
