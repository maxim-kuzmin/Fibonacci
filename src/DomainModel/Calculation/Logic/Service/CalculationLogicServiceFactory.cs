namespace Fibonacci.DomainModel.Calculation.Logic.Service;

/// <summary>
/// Фабрика сервисов логики расчёта.
/// </summary>
public class CalculationLogicServiceFactory : ICalculationLogicServiceFactory
{
  /// <inheritdoc/>
  public ICalculationLogicService CreateCalculationLogicService()
  {
    return new CalculationLogicService();
  }
}
