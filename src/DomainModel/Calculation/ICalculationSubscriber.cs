namespace Fibonacci.DomainModel.Calculation;

/// <summary>
/// Интерфейс подписчика расчёта. Нужен для получения результата расчёта из очереди сообщений.
/// </summary>
public interface ICalculationSubscriber
{
  /// <summary>
  /// Подписаться. Нужно выполнить только один раз, чтобы подписаться на получение сообщений из очереди.
  /// </summary>
  /// <returns></returns>
  Task Subscribe();

  /// <summary>
  /// Получить задачу на получение результата следующего расчёта на основании результата предыдущего.
  /// </summary>
  /// <param name="previousCalculationResult">Результат предыдущего расчёта.</param>
  /// <param name="cancellationToken">Токен отмены.</param>
  /// <returns>Задача на получение результата следующего расчёта.</returns>
  Task<Task<CalculationResult>> GetNextCalculationResultTask(
    CalculationResult previousCalculationResult,
    CancellationToken cancellationToken);
}
