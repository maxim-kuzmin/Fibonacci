namespace Fibonacci.DomainModel.Calculation.Result;

/// <summary>
/// Интерфейс публикатора результата расчёта.
/// </summary>
public interface ICalculationResultPublisher
{
  /// <summary>
  /// Опубликовать результат расчёта.
  /// </summary>
  /// <param name="calculationId">Идентификатор расчёта.</param>
  /// <param name="calculationResult">Результат расчёта.</param>
  /// <param name="cancellationToken">Токен отмены.</param>
  /// <returns></returns>
  Task PublishCalculationResult(
    Guid calculationId,
    CalculationResult calculationResult,
    CancellationToken cancellationToken);
}
