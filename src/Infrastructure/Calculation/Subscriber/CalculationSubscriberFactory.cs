namespace Fibonacci.Infrastructure.Calculation.Subscriber;

/// <summary>
/// Фабрика подписчиков расчёта.
/// </summary>
/// <param name="_bus">Шина.</param>
/// <param name="_httpClientFactory">Фабрика HTTP-клиентов.</param>
public class CalculationSubscriberFactory(IBus _bus, IHttpClientFactory _httpClientFactory) : ICalculationSubscriberFactory
{
  /// <inheritdoc/>
  public ICalculationSubscriber CreateCalculationSubscriber(Guid calculationId)
  {
    return new CalculationSubscriber(calculationId, _bus, _httpClientFactory);
  }
}
