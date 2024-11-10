namespace Fibonacci.Infrastructure.Calculation.Fakes;

/// <summary>
/// Подделка фабрики подписчиков расчёта. Нужна для получения подписчиков расчёта без использования очереди сообщений.
/// </summary>
/// <param name="_calculationService">Сервис расчёта.</param>
public class CalculationSubscriberFactoryFake(ICalculationService _calculationService) : ICalculationSubscriberFactory
{
  /// <inheritdoc/>
  public ICalculationSubscriber CreateCalculationSubscriber(Guid calculationId)
  {
    return new CalculationSubscriberFake(calculationId, _calculationService);
  }
}
