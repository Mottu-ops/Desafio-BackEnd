using System.Diagnostics;

namespace Motorcycle.Service.Interfaces;

public interface ILoggerService {
    public void logWarming(string msg, StackTrace trace);
    public void logInfo(string msg, StackTrace trace);
    public void logError(string msg, StackTrace trace);
    public void logException(string msg, StackTrace trace);
}