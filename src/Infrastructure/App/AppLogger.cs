namespace Fibonacci.Infrastructure.App;

/// <summary>
/// Логгер приложения.
/// </summary>
public class AppLogger
{
  static AppLogger()
  {
    Log.Logger = new LoggerConfiguration()
      .Enrich.FromLogContext()
      .WriteTo.Console()
      .CreateLogger();
  }

  /// <summary>
  /// Создать.
  /// </summary>
  /// <typeparam name="T">Тип логгера.</typeparam>
  /// <returns>Логгер.</returns>
  public static Microsoft.Extensions.Logging.ILogger Create<T>()
  {
    return new SerilogLoggerFactory(Log.Logger).CreateLogger<T>();
  }

  /// <summary>
  /// Закрыть и смыть.
  /// </summary>
  public static void CloseAndFlush()
  {
    Log.CloseAndFlush();
  }
}
