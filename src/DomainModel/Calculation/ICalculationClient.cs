namespace Fibonacci.DomainModel.Calculation;

/// <summary>
/// Интерфейс клиента расчёта.
/// </summary>
public interface ICalculationClient
{
  /// <summary>
  /// Получить результат следующего расчёта с указанным идентификатором на основании результата предыдущего.
  /// </summary>
  /// <param name="calculationId">Идентификатор расчёта.</param>
  /// <param name="previousCalculationResult">Результат предыдущего расчёта.</param>
  /// <returns>Результат следующего расчёта.</returns>
  CalculationResult GetNextCalculationResult(Guid calculationId, CalculationResult previousCalculationResult);
}
