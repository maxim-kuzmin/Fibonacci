var logger = AppLogger.Create<Program>();

try
{
  logger.LogInformation("Starting application");

  var builder = WebApplication.CreateBuilder(args);

  var appConfigSection = builder.Configuration.GetSection(AppConfigOptions.SectionKey);

  var appConfigOptions = appConfigSection.CreateAppConfigOptions();

  builder.Services
    .AddAppDomainModelLayer(logger)
    .AddAppDomainUseCasesLayer(logger)
    .AddAppInfrastructureLayer(logger, builder.Configuration, appConfigOptions.RabbitMQ)
    .AddAppUILayer(logger, appConfigSection);

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

public partial class Program { }
