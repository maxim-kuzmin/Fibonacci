namespace Fibonacci.Infrastructure.App;

/// <summary>
/// Настройки приложения.
/// </summary>
public static class AppSettings
{
  /// <summary>
  /// Имя HTTP-клиента издателя расчёта.
  /// </summary>
  public const string CalculationPublisherHttpClientName = "CalculationPublisher";

  /// <summary>
  /// URL отправки результата расчёта.
  /// </summary>
  public const string CalculationSendResultUrl = "calculation/send-result";
}
