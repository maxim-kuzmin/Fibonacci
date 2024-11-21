namespace Fibonacci.DomainModel.App;

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
  public static IServiceCollection AddAppDomainModelLayer(
    this IServiceCollection services,
    ILogger logger)
  {
    services.AddTransient<ICalculationLogicServiceFactory, CalculationLogicServiceFactory>();    

    logger.LogInformation("DomainModel layer added");

    return services;
  }
}
