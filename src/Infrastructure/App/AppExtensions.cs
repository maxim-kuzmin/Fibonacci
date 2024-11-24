namespace Fibonacci.Infrastructure.App;

/// <summary>
/// Расширения приложения.
/// </summary>
public static class AppExtensions
{
  /// <summary>
  /// Добавить слой инфраструктуры приложения.
  /// </summary>
  /// <param name="services">Сервисы.</param>
  /// <param name="logger">Логгер.</param>
  /// <param name="configuration">Конфигурация.</param>
  /// <param name="appConfigOptionsRabbitMQ">Параметры конфигурации приложения для RabbitMQ.</param>
  /// <returns>Сервисы.</returns>
  public static IServiceCollection AddAppInfrastructureLayer(
    this IServiceCollection services,
    Microsoft.Extensions.Logging.ILogger logger,
    IConfiguration configuration,
    AppConfigOptionsRabbitMQ appConfigOptionsRabbitMQ)
  {
    services.AddSerilog(config => config.ReadFrom.Configuration(configuration));

    services.AddEasyNetQ($"host={appConfigOptionsRabbitMQ.Hostname}:{appConfigOptionsRabbitMQ.Port};username={appConfigOptionsRabbitMQ.Username};password={appConfigOptionsRabbitMQ.Password}")
      .UseSystemTextJson();

    services.AddSingleton<IAppBus, AppEasyNetQBus>();
    // /makc// services.AddSingleton<IAppBus, AppInMemoryBus>();
    services.AddSingleton<ICalculationCurrentResultPublisher, CalculationAppBusCurrentResultPublisher>();
    services.AddTransient<ICalculationService, CalculationService>();    

    logger.LogInformation("Infrastructure layer added");

    return services;
  }
}
