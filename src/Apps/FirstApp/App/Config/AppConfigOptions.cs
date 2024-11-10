namespace Fibonacci.Apps.FirstApp.App.Config;

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
  /// URL второго приложения.
  /// </summary>
  public string SecondAppUrl { get; set; } = string.Empty;

  /// <summary>
  /// Параметры конфигурации приложения для RabbitMQ.
  /// </summary>
  public AppConfigOptionsRabbitMQ RabbitMQ { get; set; } = null!;
}
