namespace Fibonacci.Apps.FirstApp.App;

/// <summary>
/// Расширения приложения.
/// </summary>
public static class AppExtensions
{
  /// <summary>
  /// Добавить уровень пользовательского интерфейса приложения.
  /// </summary>
  /// <param name="services">Сервисы.</param>
  /// <param name="logger">Логгер.</param>
  /// <param name="secondAppUrl">URL второго приложения.</param>
  /// <param name="calculationCount">Количество расчётов.</param>
  /// <returns>Сервисы.</returns>
  public static IServiceCollection AddAppUILayer(
    this IServiceCollection services,
    ILogger logger,
    string secondAppUrl,
    int calculationCount)
  {
    ArgumentOutOfRangeException.ThrowIfLessThan(calculationCount, 1);

    services.AddSingleton(_ => new CalculationOptions(calculationCount, 0));

    services.AddHostedService<CalculationWorker>();

    services.AddHttpClient(
        AppSettings.CalculationNextResultPublisherHttpClientName,
        client =>
        {
          client.BaseAddress = new Uri(secondAppUrl);

          client.DefaultRequestHeaders.UserAgent.ParseAdd(nameof(FirstApp));
        })
        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
        {
          ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        });

    services.AddSingleton<ICalculationMonitor, CalculationMonitor>();    
    services.AddSingleton<ICalculationResultConsumerFactory, CalculationResultConsumerFactory>();
    services.AddTransient<ICalculationNextResultPublisher, CalculationHttpClientNextResultPublisher>();
    // //makc// services.AddTransient<ICalculationNextResultPublisher, CalculationAppBusNextResultPublisher>(); // Для выполнения расчёта без второго приложения
    services.AddTransient<ICalculationService, CalculationService>();

    logger.LogInformation("UI layer added");

    return services;
  }

  /// <summary>
  /// Создать параметры конфигурации приложения.
  /// </summary>
  /// <param name="appConfigSection">Раздел конфигурации приложения.</param>
  /// <returns>Параметры конфигурации приложения.</returns>
  public static AppConfigOptions CreateAppConfigOptions(this IConfigurationSection appConfigSection)
  {
    var result = new AppConfigOptions();

    appConfigSection.Bind(result);

    return result;
  }
}
