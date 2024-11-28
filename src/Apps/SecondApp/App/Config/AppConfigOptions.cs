namespace Fibonacci.Apps.SecondApp.App.Config;

/// <summary>
/// Параметры конфигурации приложения.
/// </summary>
public record AppConfigOptions
{
  /// <summary>
  /// Ключ раздела.
  /// </summary>
  public const string SectionKey = "App";

  /// <summary>
  /// Наблюдаемость.
  /// </summary>
  public AppConfigOptionsObservability Observability { get; set; } = null!;

  /// <summary>
  /// RabbitMQ.
  /// </summary>
  public AppConfigOptionsRabbitMQ RabbitMQ { get; set; } = null!;
}
