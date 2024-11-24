namespace Fibonacci.Apps.FirstApp.Calculation;

/// <summary>
/// Интерфейс монитора расчётов.
/// </summary>
public interface ICalculationMonitor
{
  /// <summary>
  /// Отобразить.
  /// </summary>
  /// <param name="calculationId">Идентификатор расчёта.</param>
  /// <param name="calculationResult">Результат расчёта.</param>
  void Display(Guid calculationId, CalculationResult calculationResult);
}
