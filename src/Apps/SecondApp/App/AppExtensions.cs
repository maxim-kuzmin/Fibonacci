namespace Fibonacci.Apps.SecondApp.App;

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
  /// <returns>Сервисы.</returns>
  public static IServiceCollection AddAppUILayer(this IServiceCollection services, ILogger logger)
  {
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

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

  /// <summary>
  /// Использовать уровень пользовательского интерфейса приложения.
  /// </summary>
  /// <param name="app">Приложение.</param>
  /// <param name="logger">Логгер.</param>
  /// <returns>Приложение.</returns>
  public static WebApplication UseAppUILayer(this WebApplication app, ILogger logger)
  {
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseMiddleware<AppTraceIdResponseHeaderMiddleware>();

    app.MapGroup($"/{AppSettings.CalculationApiPath}").MapCalculationApi().WithTags("Calculation Endpoints");

    logger.LogInformation("UI layer used");

    return app;
  }
}
