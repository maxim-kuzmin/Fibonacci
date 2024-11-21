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
  /// <param name="appConfigOptions">Параметры конфигурации приложения.</param>
  /// <param name="appConfigSection">Раздел конфигурации приложения.</param>
  /// <param name="calculationCount">Количество расчётов.</param>
  /// <returns>Сервисы.</returns>
  public static IServiceCollection AddAppUILayer(
    this IServiceCollection services,
    ILogger logger,
    AppConfigOptions appConfigOptions,
    IConfigurationSection appConfigSection,
    int calculationCount)
  {
    ArgumentOutOfRangeException.ThrowIfLessThan(calculationCount, 1);

    services.Configure<AppConfigOptions>(appConfigSection);

    services.AddSingleton(_ => new CalculationCount(calculationCount));

    services.AddHostedService<CalculationWorker>();

    services.AddHttpClient(
        AppSettings.CalculationPublisherHttpClientName,
        client =>
        {
          client.BaseAddress = new Uri(appConfigOptions.SecondAppUrl);

          client.DefaultRequestHeaders.UserAgent.ParseAdd(nameof(FirstApp));
        })
        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
        {
          ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        });

    services.AddSingleton<ICalculationSubscriberFactory, CalculationSubscriberFactory>();
    // //makc// services.AddSingleton<ICalculationSubscriberFactory, Calculation.Fakes.CalculationSubscriberFactoryFake>();

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
