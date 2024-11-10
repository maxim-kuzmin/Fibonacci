namespace Fibonacci.Infrastructure.Calculation;

/// <summary>
/// Издатель расчёта. Нужен для отправки результата расчёта в очередь сообщений.
/// </summary>
/// <param name="_bus">Шина.</param>
public class CalculationPublisher(IBus _bus) : ICalculationPublisher
{
  /// <inheritdoc/>
  public Task PublishNextCalculationResult(Guid calculationId, CalculationResult nextCalculationResult)
  {
    var calculationResultDTO = nextCalculationResult.ToCalculationResultDTO(calculationId);

    return _bus.PubSub.PublishAsync(calculationResultDTO, (config) =>
    {
      config.WithTopic(calculationId.ToString());
    });
  }
}
