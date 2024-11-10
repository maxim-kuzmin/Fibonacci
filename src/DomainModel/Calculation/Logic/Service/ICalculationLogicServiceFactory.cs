namespace Fibonacci.DomainModel.Calculation.Logic.Service;

/// <summary>
/// Интерфейс фабрики сервисов логики расчёта.
/// </summary>
public interface ICalculationLogicServiceFactory
{
  /// <summary>
  /// Создать сервис логики расчёта.
  /// </summary>
  /// <returns>Сервис логики расчёта.</returns>
  ICalculationLogicService CreateCalculationLogicService();
}
