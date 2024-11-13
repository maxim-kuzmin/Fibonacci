namespace Fibonacci.DomainModel.Calculation;

/// <summary>
/// Интерфейс подписчика расчёта. Нужен для получения результата расчёта из очереди сообщений.
/// </summary>
public interface ICalculationSubscriber
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
