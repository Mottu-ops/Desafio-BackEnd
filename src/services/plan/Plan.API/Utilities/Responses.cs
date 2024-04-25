using Plan.API.ViewModels;

namespace Plan.API.Utilities {
    public static class Responses {
        public static ResultViewModel ApplicationErrorMessage(dynamic data) {
            return new ResultViewModel {
                Message = "There is some internal error application, please try again later",
                Success = false,
                Data = data
            };
        }
        public static ResultViewModel DomainErrorMessage(string message, IReadOnlyCollection<string> errors){

            return new ResultViewModel {
                Message = message,
                Success = true,
                Data = errors
            };
        }
    }
}