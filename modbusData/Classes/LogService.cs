using Serilog;
using Serilog.Events;
using System.Windows.Forms;

public class LoggerService
{
    private readonly LoggerConfiguration _loggerConfiguration;
    private readonly ILogger _logger;
    string logFilePath = Application.StartupPath + "\\sirilog.log";

    public LoggerService()
    {
        _loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.File(logFilePath)
            .WriteTo.Console();

        _logger = _loggerConfiguration.CreateLogger();
    }

    public void LogMessage(LogType logType, ErrorType errorType, string message)
    {
        LogEventLevel logLevel = GetLogLevel(logType);

        switch (errorType)
        {
            case ErrorType.Information:
                _logger.Information(message);
                break;
            case ErrorType.Warning:
                _logger.Warning(message);
                break;
            case ErrorType.Error:
                _logger.Error(message);
                break;
            case ErrorType.Fatal:
                _logger.Fatal(message);
                break;
            default:
                _logger.Write(logLevel, message);
                break;
        }
    }

    private LogEventLevel GetLogLevel(LogType logType)
    {
        switch (logType)
        {
            case LogType.Verbose:
                return LogEventLevel.Verbose;
            case LogType.Debug:
                return LogEventLevel.Debug;
            case LogType.Information:
                return LogEventLevel.Information;
            case LogType.Warning:
                return LogEventLevel.Warning;
            case LogType.Error:
                return LogEventLevel.Error;
            case LogType.Fatal:
                return LogEventLevel.Fatal;
            default:
                return LogEventLevel.Information; // Default to Information level
        }
    }
}

public enum LogType
{
    Verbose,
    Debug,
    Information,
    Warning,
    Error,
    Fatal
}

public enum ErrorType
{
    Information,
    Warning,
    Error,
    Fatal
}