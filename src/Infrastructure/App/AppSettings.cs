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
  /// Путь API расчёта.
  /// </summary>
  public const string CalculationApiPath = "calculation";

  /// <summary>
  /// Путь отправки результата API расчёта.
  /// </summary>
  public const string CalculationApiSendResultPath = "send-result";

  /// <summary>
  /// URL отправки результата API расчёта.
  /// </summary>
  public const string CalculationApiSendResultUrl = $"{CalculationApiPath}/{CalculationApiSendResultPath}";
}
