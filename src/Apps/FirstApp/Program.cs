var logger = AppLogger.Create<Program>();

try
{
  logger.LogInformation("Starting application");

  var builder = Host.CreateApplicationBuilder(args);

  var appConfigSection = builder.Configuration.GetSection(AppConfigOptions.SectionKey);

  var appConfigOptions = appConfigSection.CreateAppConfigOptions();

  int calculationCount = GetCalculationCount(args, 1);

  builder.Services
    .AddAppDomainModelLayer(logger)
    .AddAppDomainUseCasesLayer(logger)
    .AddAppInfrastructureLayer(
      logger,
      builder.Configuration,
      appConfigOptions.Observability,
      appConfigOptions.RabbitMQ)
    .AddAppUILayer(logger, appConfigOptions.SecondAppUrl, calculationCount);

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

static int GetCalculationCount(string[] args, int calculationCount)
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

public partial class Program { }
