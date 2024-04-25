namespace BusConnections.Events.Enums
{
    public class ErrorMessage
    {
        public ErrorMessage(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }

        public static ErrorMessage ConnectionProblem = new ErrorMessage("Connection error.");
        public static ErrorMessage ConnectionCreationFail = new ErrorMessage("Connection could not be created.");
        public static ErrorMessage ConnectionBlocked = new ErrorMessage("The connection is blocked. Trying to reconnect.");
        public static ErrorMessage ConnectionClosed = new ErrorMessage("The connection has been closed. Trying to reconnect.");
        public static ErrorMessage NoValidConnectionToCreateModel = new ErrorMessage("No valid connection was found to create the model.");
        public static ErrorMessage TheResourcesUsedDeleted = new ErrorMessage("The resources used have been deleted.");
    }
}