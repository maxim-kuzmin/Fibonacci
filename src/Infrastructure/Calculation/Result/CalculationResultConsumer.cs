namespace Fibonacci.Infrastructure.Calculation.Result;

/// <summary>
/// Потребитель результата расчёта.
/// Нужен для получения результата расчёта из шины приложения.
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

    Task.Run(() => Consume(previousCalculationResult, cancellationToken), cancellationToken);

    return _taskCompletionSource.Task;
  }

  private Task OnMessage(CalculationResultDTO calculationResultDTO, CancellationToken cancellationToken)
  {
    _taskCompletionSource.SetResult(calculationResultDTO.ToCalculationResult());

    return Task.CompletedTask;
  }

  private async Task Consume(CalculationResult calculationResult, CancellationToken cancellationToken)
  {
    if (!_isSubscribed && !cancellationToken.IsCancellationRequested)
    {
      var task = _appBus.Subscribe<CalculationResultDTO>(_calculationId.ToString(), OnMessage, cancellationToken);

      await task.ConfigureAwait(false);

      _isSubscribed = true;
    }

    if (!cancellationToken.IsCancellationRequested)
    {
      var task = _сalculationNextResultPublisher.PublishCalculationResult(
        _calculationId,
        calculationResult,
        cancellationToken);

      await task.ConfigureAwait(false);
    }
  }
}
