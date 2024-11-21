namespace Fibonacci.Apps.SecondApp.App;

/// <summary>
/// Расширения приложения.
/// </summary>
public static class AppExtensions
{
  /// <summary>
  /// Добавить уровень хоста приложения.
  /// </summary>
  /// <param name="services">Сервисы.</param>
  /// <param name="logger">Логгер.</param>
  /// <param name="appConfigSection">Раздел конфигурации приложения.</param>
  /// <returns>Сервисы.</returns>
  public static IServiceCollection AddAppHostLayer(
    this IServiceCollection services,
    ILogger logger,
    IConfigurationSection appConfigSection)
  {
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.Configure<AppConfigOptions>(appConfigSection);

    services.AddSingleton<ICalculationPublisher, CalculationPublisher>();

    logger.LogInformation("{Layer} layer added", nameof(SecondApp));

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

  /// <summary>
  /// Использовать уровень хоста приложения.
  /// </summary>
  /// <param name="app">Приложение.</param>
  /// <param name="logger">Логгер.</param>
  /// <returns>Приложение.</returns>
  public static WebApplication UseAppHostLayer(this WebApplication app, ILogger logger)
  {
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.MapPost($"/{AppSettings.CalculationSendResultUrl}", CalculationSendResult)
      .WithName(nameof(CalculationSendResult))
      .WithOpenApi();

    logger.LogInformation("{Layer} layer used", nameof(AppHost));

    return app;
  }

  private static Task CalculationSendResult(IMediator mediator, CalculationSendResultActionCommand command)
  {
    return mediator.Send(command);
  }
}
