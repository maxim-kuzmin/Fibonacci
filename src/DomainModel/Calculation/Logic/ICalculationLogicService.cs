namespace Fibonacci.DomainModel.Calculation.Logic;

/// <summary>
/// Интерфейс сервиса логики расчёта.
/// </summary>
public interface ICalculationLogicService
{
  /// <summary>
  /// Получить результат следующего расчёта на основании результата предыдущего.
  /// </summary>
  /// <param name="previousCalculationResult">Результат предыдущего расчёта.</param>
  /// <returns>Результат следующего расчёта.</returns>
  CalculationResult GetNextCalculationResult(CalculationResult previousCalculationResult);
}
