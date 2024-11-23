namespace Fibonacci.Infrastructure.App;

/// <summary>
/// Настройки приложения.
/// </summary>
public static class AppSettings
{
  /// <summary>
  /// Имя HTTP-клиента публикатора следующего результата расчёта.
  /// </summary>
  public const string CalculationNextResultPublisherHttpClientName = "CalculationNextResultPublisher";

  /// <summary>
  /// URL отправки результата расчёта.
  /// </summary>
  public const string CalculationSendResultUrl = "calculation/send-result";
}
