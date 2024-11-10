namespace Fibonacci.Apps.FirstApp.App;

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

      var builder = Host.CreateApplicationBuilder(args);

      var appConfigSection = builder.Configuration.GetSection(AppConfigOptions.SectionKey);

      var appConfigOptions = appConfigSection.CreateAppConfigOptions();

      int calculationCount = GetCalculationCount(args, 1);

      builder.Services
        .AddAppHostLayer(logger, appConfigOptions, appConfigSection, calculationCount)
        .AddAppInfrastructureLayer(logger, builder.Configuration, appConfigOptions.RabbitMQ);

      var app = builder.Build();

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

  private static int GetCalculationCount(string[] args, int calculationCount)
  {
    int minCalculationCount = calculationCount;

    if (args.Length > 0)
    {
      if (!int.TryParse(args[0], out calculationCount))
      {
        throw new ArgumentException("Not a number", nameof(calculationCount));
      }      

      if (calculationCount < minCalculationCount)
      {
        ArgumentOutOfRangeException.ThrowIfLessThan(calculationCount, minCalculationCount);
      }
    }

    return calculationCount;
  }
}
