namespace Fibonacci.Infrastructure.Calculation.Result.Consumer;

/// <summary>
/// Фабрика потребителей результата расчёта.
/// </summary>
/// <param name="_appBus">Шина приложения.</param>
/// <param name="_calculationNextResultPublisher">Публикатор следующего результата расчёта.</param>
public class CalculationResultConsumerFactory(
  IAppBus _appBus,
  ICalculationNextResultPublisher _calculationNextResultPublisher) : ICalculationResultConsumerFactory
{
  /// <inheritdoc/>
  public ICalculationResultConsumer CreateCalculationResultConsumer(Guid calculationId)
  {
    return new CalculationResultConsumer(calculationId, _appBus, _calculationNextResultPublisher);
  }
}
