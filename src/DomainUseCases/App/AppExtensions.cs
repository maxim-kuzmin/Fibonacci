using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fibonacci.DomainUseCases.App;

/// <summary>
/// Расширения приложения.
/// </summary>
public static class AppExtensions
{
  /// <summary>
  /// Добавить слой сценариев использования домена приложения.
  /// </summary>
  /// <param name="services">Сервисы.</param>
  /// <param name="logger">Логгер.</param>
  /// <returns>Сервисы.</returns>
  public static IServiceCollection AddAppDomainUseCasesLayer(
    this IServiceCollection services,
    ILogger logger)
  {
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

    services.AddTransient<ICalculationLogicServiceFactory, CalculationLogicServiceFactory>();

    logger.LogInformation("{Layer} layer added", nameof(DomainUseCases));

    return services;
  }
}
