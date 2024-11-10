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
    ArgumentNullException.ThrowIfNull(logger);
    ArgumentNullException.ThrowIfNull(configuration);
    ArgumentNullException.ThrowIfNull(appConfigOptionsRabbitMQ);

    services.AddSerilog(config => config.ReadFrom.Configuration(configuration));

    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

    services.AddEasyNetQ($"host={appConfigOptionsRabbitMQ.Hostname}:{appConfigOptionsRabbitMQ.Port};username={appConfigOptionsRabbitMQ.Username};password={appConfigOptionsRabbitMQ.Password}")
      .UseSystemTextJson();

    services.AddTransient<ICalculationLogicServiceFactory, CalculationLogicServiceFactory>();
    services.AddTransient<ICalculationService, CalculationService>();
    services.AddSingleton<ICalculationClient, CalculationClient>();
    services.AddSingleton<ICalculationPublisher, CalculationPublisher>();

    logger.LogInformation("{Layer} layer added", nameof(Infrastructure));

    return services;
  }
}
