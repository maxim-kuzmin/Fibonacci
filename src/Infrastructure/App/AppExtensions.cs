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
  /// <param name="appConfigOptionsObservability">Параметры конфигурации приложения для наблюдаемости.</param>
  /// <returns>Сервисы.</returns>
  public static IServiceCollection AddAppInfrastructureLayer(
    this IServiceCollection services,
    Microsoft.Extensions.Logging.ILogger logger,    
    IConfiguration configuration,
    AppConfigOptionsObservability appConfigOptionsObservability,
    AppConfigOptionsRabbitMQ appConfigOptionsRabbitMQ)
  {
    services.AddAppLogging(configuration, appConfigOptionsObservability);

    services.AddOpenTelemetry()
      .ConfigureResource(resource => resource.AddService(appConfigOptionsObservability.ServiceName))
      .AddAppMetricsToOpenTelemetry(appConfigOptionsObservability)
      .AddAppTracingToOpenTelemetry(appConfigOptionsObservability);

    services.AddEasyNetQ($"host={appConfigOptionsRabbitMQ.Hostname}:{appConfigOptionsRabbitMQ.Port};username={appConfigOptionsRabbitMQ.Username};password={appConfigOptionsRabbitMQ.Password}")
      .UseSystemTextJson();

    services.AddSingleton<IAppBus, AppEasyNetQBus>();
    // //makc// services.AddSingleton<IAppBus, AppInMemoryBus>(); // Для работы без использования очереди сообщений при выполнении расчёта без второго приложения
    services.AddSingleton<ICalculationCurrentResultPublisher, CalculationAppBusCurrentResultPublisher>();
    services.AddTransient<ICalculationService, CalculationService>();    

    logger.LogInformation("Infrastructure layer added");

    return services;
  }

  private static IServiceCollection AddAppLogging(
    this IServiceCollection services,
    IConfiguration configuration,
    AppConfigOptionsObservability appConfigOptionsObservability)
  {
    services.AddSerilog((serviceProvider, config) =>
    {
      config.ReadFrom.Configuration(configuration)
          .ReadFrom.Services(serviceProvider)
          .Enrich.FromLogContext()
          .Enrich.WithProperty("ApplicationName", appConfigOptionsObservability.ServiceName)
          .WriteTo.OpenTelemetry(c =>
          {
            c.Endpoint = appConfigOptionsObservability.CollectorUrl;
            c.Protocol = OtlpProtocol.Grpc;
            c.IncludedData = IncludedData.TraceIdField | IncludedData.SpanIdField | IncludedData.SourceContextAttribute;
            c.ResourceAttributes = new Dictionary<string, object>
            {
              {"service.name", appConfigOptionsObservability.ServiceName},
              {"index", 10},
              {"flag", true},
              {"value", 3.14}
            };
          });
    });

    return services;
  }

  private static OpenTelemetryBuilder AddAppTracingToOpenTelemetry(
    this OpenTelemetryBuilder builder,
    AppConfigOptionsObservability appConfigOptionsObservability)
  {
    if (!appConfigOptionsObservability.IsTracingCollectionEnabled)
    {
      return builder;
    }

    builder.WithTracing(tracingConfig =>
    {
      tracingConfig
          .SetErrorStatusOnException()
          .SetSampler(new AlwaysOnSampler())
          .AddAspNetCoreInstrumentation(options =>
          {
            options.RecordException = true;
          })
          .AddOtlpExporter(exporterConfig =>
          {
            exporterConfig.Endpoint = new Uri(appConfigOptionsObservability.CollectorUrl);
            exporterConfig.ExportProcessorType = ExportProcessorType.Batch;
            exporterConfig.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
          });
    });

    return builder;
  }

  private static OpenTelemetryBuilder AddAppMetricsToOpenTelemetry(
    this OpenTelemetryBuilder builder,
    AppConfigOptionsObservability appConfigOptionsObservability)
  {
    if (!appConfigOptionsObservability.IsMetricsCollectionEnabled)
    {
      return builder;
    }

    builder.WithMetrics(metricsConfig =>
    {
      metricsConfig
          .AddAspNetCoreInstrumentation()
          .AddOtlpExporter(exporterConfig =>
          {
            exporterConfig.Endpoint = new Uri(appConfigOptionsObservability.CollectorUrl);
            exporterConfig.ExportProcessorType = ExportProcessorType.Batch;
            exporterConfig.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
          });
    });

    return builder;
  }
}
