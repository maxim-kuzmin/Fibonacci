namespace Fibonacci.DomainModel.Calculation.Subscriber;

/// <summary>
/// Интерфейс фабрики подписчиков расчёта.
/// </summary>
public interface ICalculationSubscriberFactory
{
  /// <summary>
  /// Создать подписчика расчёта.
  /// </summary>
  /// <param name="calculationId">Идентификатор рачсёта.</param>
  /// <returns>Подписчик расчёта.</returns>
  ICalculationSubscriber CreateCalculationSubscriber(Guid calculationId);
}
