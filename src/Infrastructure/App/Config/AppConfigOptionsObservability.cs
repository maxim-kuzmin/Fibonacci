namespace Fibonacci.Infrastructure.App.Config;

/// <summary>
/// Параметры конфигурации приложения для наблюдаемости.
/// </summary>
/// <param name="ServiceName">Имя сервиса.</param>
/// <param name="CollectorUrl">URL коллектора.</param>
/// <param name="IsLogsCollectionEnabled">Признак включения сбора логов.</param>
/// <param name="IsTracingCollectionEnabled">Признак включения сбора трейсинга.</param>
/// <param name="IsMetricsCollectionEnabled">Признак включения сбора метрик.</param>
public record AppConfigOptionsObservability(
  string ServiceName,
  string CollectorUrl,
  bool IsLogsCollectionEnabled,
  bool IsTracingCollectionEnabled,
  bool IsMetricsCollectionEnabled);
