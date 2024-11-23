namespace Fibonacci.DomainModel.Calculation.Result;

/// <summary>
/// Интерфейс потребителя результата расчёта.
/// </summary>
public interface ICalculationResultConsumer
{
  /// <summary>
  /// Получить результат следующего расчёта на основании результата предыдущего.
  /// </summary>
  /// <param name="previousCalculationResult">Результат предыдущего расчёта.</param>
  /// <param name="cancellationToken">Токен отмены.</param>
  /// <returns>Результат следующего расчёта.</returns>
  Task<CalculationResult> GetNextCalculationResult(
    CalculationResult previousCalculationResult,
    CancellationToken cancellationToken);
}
