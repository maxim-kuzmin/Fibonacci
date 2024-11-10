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
  /// Параметры конфигурации приложения для RabbitMQ.
  /// </summary>
  public AppConfigOptionsRabbitMQ RabbitMQ { get; set; } = null!;
}
