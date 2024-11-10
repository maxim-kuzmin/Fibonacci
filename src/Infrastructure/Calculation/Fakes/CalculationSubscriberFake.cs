namespace Fibonacci.Infrastructure.Calculation.Fakes;

/// <summary>
/// Подделка подписчика расчёта. Нужна для получения результата расчёта без использования очереди сообщений.
/// </summary>
/// <param name="_calculationId">Идентификатор расчёта.</param>
/// <param name="_calculationService">Сервис расчёта.</param>
public class CalculationSubscriberFake(Guid _calculationId, ICalculationService _calculationService) : ICalculationSubscriber
{
  private CalculationClient? _calculationClient;

  /// <inheritdoc/>
  public Task Subscribe()
  {
    _calculationClient = new(_calculationService);

    return Task.CompletedTask;
  }

  /// <inheritdoc/>
  public Task<Task<CalculationResult>> GetNextCalculationResultTask(
    CalculationResult previousCalculationResult,
    CancellationToken cancellationToken)
  { 
    if (_calculationClient == null)
    {
      throw new InvalidOperationException("Should subscribe");
    }

    var task = Task.FromResult(_calculationClient.GetNextCalculationResult(_calculationId, previousCalculationResult));

    return Task.FromResult(task);
  }
}
