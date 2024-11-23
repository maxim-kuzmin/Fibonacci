namespace Fibonacci.DomainModel.Calculation.Result.Consumer;

/// <summary>
/// Интерфейс фабрики потребителей результата расчёта.
/// </summary>
public interface ICalculationResultConsumerFactory
{
  /// <summary>
  /// Создать потребителя результата расчёта.
  /// </summary>
  /// <param name="calculationId">Идентификатор расчёта.</param>
  /// <returns>Потребитель расчёта.</returns>
  ICalculationResultConsumer CreateCalculationResultConsumer(Guid calculationId);
}
