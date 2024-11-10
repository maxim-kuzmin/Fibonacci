namespace Fibonacci.DomainModel.Calculation;

/// <summary>
/// Интерфейс издателя расчёта. Нужен для отправки результата расчёта в очередь сообщений.
/// </summary>
public interface ICalculationPublisher
{
  /// <summary>
  /// Опубликовать результат следующего расчёта.
  /// </summary>
  /// <param name="calculationId">Идентификатор расчёта.</param>
  /// <param name="nextCalculationResult">Результат следующего расчёта.</param>
  /// <returns></returns>
  Task PublishNextCalculationResult(Guid calculationId, CalculationResult nextCalculationResult);
}
