namespace Fibonacci.Infrastructure.Calculation.Local.Subscriber;

/// <summary>
/// Локальная фабрика подписчиков расчёта. Нужна для получения подписчиков расчёта без использования очереди сообщений.
/// </summary>
/// <param name="_calculationService">Сервис расчёта.</param>
public class CalculationLocalSubscriberFactory(ICalculationService _calculationService) : ICalculationSubscriberFactory
{
  /// <inheritdoc/>
  public ICalculationSubscriber CreateCalculationSubscriber(Guid calculationId)
  {
    return new CalculationLocalSubscriber(calculationId, _calculationService);
  }
}
