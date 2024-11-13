namespace Fibonacci.Infrastructure.Calculation;

/// <summary>
/// Подписчик расчёта. Нужен для получения результата расчёта из очереди сообщений.
/// </summary>
/// <param name="_calculationId">Идентификатор расчёта.</param>
/// <param name="_bus">Шина.</param>
/// <param name="_httpClientFactory">Фабрика HTTP-клиентов.</param>
public class CalculationSubscriber(
  Guid _calculationId,
  IBus _bus,
  IHttpClientFactory _httpClientFactory) : ICalculationSubscriber
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

  private async Task SendCalculationResult(CalculationResult calculationResult, CancellationToken cancellationToken)
  {
    using var httpClient = _httpClientFactory.CreateClient(AppSettings.CalculationPublisherHttpClientName);

    var calculationResultDTO = calculationResult.ToCalculationResultDTO(_calculationId);

    using var requestContent = JsonContent.Create(calculationResultDTO);

    var requestTask = httpClient.PostAsync(AppSettings.CalculationSendResultUrl, requestContent, cancellationToken);

    var responseMessage = await requestTask.ConfigureAwait(false);

    responseMessage.EnsureSuccessStatusCode();
  }

  private async Task Subscribe(CancellationToken cancellationToken)
  {
    await _bus.PubSub.SubscribeAsync<CalculationResultDTO>(
      _calculationId.ToString(),
      (calculationResultDTO) =>
      {
        _taskCompletionSource.SetResult(calculationResultDTO.ToCalculationResult());
      },
      (config) =>
      {
        config.WithAutoDelete().WithTopic(_calculationId.ToString());
      },
      cancellationToken);
  }

  private async Task SubscribeAndSendCalculationResult(
    CalculationResult calculationResult,
    CancellationToken cancellationToken)
  {
    if (!_isSubscribed && !cancellationToken.IsCancellationRequested)
    {
      await Subscribe(cancellationToken);

      _isSubscribed = true;
    }

    if (!cancellationToken.IsCancellationRequested)
    {
      await SendCalculationResult(calculationResult, cancellationToken);
    }
  }
}
