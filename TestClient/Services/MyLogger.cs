namespace WinFormsHello.Services;

using Serilog;

internal static class MyLogger
{
    public static void Init()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Debug()
            .WriteTo.Console()
            .WriteTo.File(
                "logs/app-.log",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7
            )
            .CreateLogger();
    }

    public static void Info(string message)
    {
        Log.Information(message);
    }

    public static void Debug(string message)
    {
        Log.Debug(message);
    }

    public static void Error(string message, Exception ex)
    {
        Log.Error(ex, message);
    }

    public static void Close()
    {
        Log.CloseAndFlush();
    }
}