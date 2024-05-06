using User.API.Models;

namespace User.API.Utils
{
    public class GenericResponse
    {
        public static BaseResultModel GenericApplicationError(dynamic data)
        {
            return new BaseResultModel
            {
                Message = "Internal server error, please try again later",
                Success = false,
                MetaData = data
            };
        }
        
        public static BaseResultModel DomainError(string message, IReadOnlyCollection<string> errors)
        {
            return new BaseResultModel
            {
                Message = message,
                Success = false,
                MetaData = errors
            };

        }
    }
}
