namespace Fibonacci.Infrastructure.Calculation.Result.Consumer;

/// <summary>
/// Подделка фабрики потребителей результата расчёта.
/// Нужна для получения потребителей результата расчёта без использования очереди сообщений.
/// </summary>
/// <param name="_calculationService">Сервис расчёта.</param>
public class CalculationResultConsumerFactoryFake(ICalculationService _calculationService) :
  ICalculationResultConsumerFactory
{
  /// <inheritdoc/>
  public ICalculationResultConsumer CreateCalculationResultConsumer(Guid calculationId)
  {
    return new CalculationResultConsumerFake(calculationId, _calculationService);
  }
}
