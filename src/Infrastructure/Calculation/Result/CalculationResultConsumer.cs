namespace Fibonacci.Infrastructure.Calculation.Result;

/// <summary>
/// Потребитель результата расчёта. Нужен для получения результата расчёта из очереди сообщений.
/// </summary>
/// <param name="_calculationId">Идентификатор расчёта.</param>
/// <param name="_appBus">Шина приложения.</param>
/// <param name="_сalculationNextResultPublisher">Публикатор следующего результата расчёта.</param>
public class CalculationResultConsumer(
  Guid _calculationId,
  IAppBus _appBus,
  ICalculationNextResultPublisher _сalculationNextResultPublisher) : ICalculationResultConsumer
{
  private TaskCompletionSource<CalculationResult> _taskCompletionSource = null!;

  private bool _isSubscribed = false;

  /// <inheritdoc/>
  public Task<CalculationResult> GetNextCalculationResult(
    CalculationResult previousCalculationResult,
    CancellationToken cancellationToken)
  {
    _taskCompletionSource = new TaskCompletionSource<CalculationResult>();

    Task.Run(() => SubscribeAndSendCalculationResult(previousCalculationResult, cancellationToken), cancellationToken);

    return _taskCompletionSource.Task;
  }

  private Task OnMessage(CalculationResultDTO calculationResultDTO, CancellationToken cancellationToken)
  {
    _taskCompletionSource.SetResult(calculationResultDTO.ToCalculationResult());

    return Task.CompletedTask;
  }

  private async Task SubscribeAndSendCalculationResult(
    CalculationResult calculationResult,
    CancellationToken cancellationToken)
  {
    if (!_isSubscribed && !cancellationToken.IsCancellationRequested)
    {
      await _appBus.Subscribe<CalculationResultDTO>(_calculationId.ToString(), OnMessage, cancellationToken);

      _isSubscribed = true;
    }

    if (!cancellationToken.IsCancellationRequested)
    {
      await _сalculationNextResultPublisher.PublishCalculationResult(
        _calculationId,
        calculationResult,
        cancellationToken);
    }
  }
}
