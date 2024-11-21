namespace Fibonacci.Apps.SecondApp.App;

/// <summary>
/// Хост приложения.
/// </summary>
public class AppHost
{
  /// <summary>
  /// Запустить асинхронно.
  /// </summary>
  /// <param name="args">Аргументы.</param>
  /// <returns></returns>
  public static Task RunAsync(string[] args)
  {
    var logger = AppLogger.Create<AppHost>();

    try
    {
      logger.LogInformation("Starting application");

      var builder = WebApplication.CreateBuilder(args);

      var appConfigSection = builder.Configuration.GetSection(AppConfigOptions.SectionKey);

      var appConfigOptions = appConfigSection.CreateAppConfigOptions();

      builder.Services
        .AddAppHostLayer(logger, appConfigSection)
        .AddAppDomainUseCasesLayer(logger)
        .AddAppInfrastructureLayer(logger, builder.Configuration, appConfigOptions.RabbitMQ);

      var app = builder.Build();

      app.UseAppHostLayer(logger);

      app.Run();
    }
    catch (Exception ex)
    {
      logger.LogCritical(ex, "Application terminated unexpectedly");
    }
    finally
    {
      AppLogger.CloseAndFlush();
    }

    return Task.CompletedTask;
  }
}
