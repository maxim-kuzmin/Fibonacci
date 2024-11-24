namespace Fibonacci.Infrastructure.Calculation.Result.Publishers.Next;

/// <summary>
/// Публикатор следующего результата расчёта с помощью HTTP-клиента.
/// </summary>
/// <param name="_httpClientFactory">Фабрика HTTP-клиентов.</param>
public class CalculationHttpClientNextResultPublisher(IHttpClientFactory _httpClientFactory) :
  ICalculationNextResultPublisher
{
  /// <inheritdoc/>
  public async Task PublishCalculationResult(
    Guid calculationId,
    CalculationResult calculationResult,
    CancellationToken cancellationToken)
  {
    using var httpClient = _httpClientFactory.CreateClient(AppSettings.CalculationNextResultPublisherHttpClientName);

    var command = calculationResult.ToCalculationSendResultActionCommand(calculationId);

    using var requestContent = JsonContent.Create(command);

    var requestTask = httpClient.PostAsync(AppSettings.CalculationSendResultUrl, requestContent, cancellationToken);

    var responseMessage = await requestTask.ConfigureAwait(false);

    responseMessage.EnsureSuccessStatusCode();
  }
}
