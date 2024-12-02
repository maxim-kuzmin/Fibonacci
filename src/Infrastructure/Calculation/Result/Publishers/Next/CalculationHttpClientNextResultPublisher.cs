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

    using var httpRequestContent = JsonContent.Create(command);

    var httpRequestTask = httpClient.PostAsync(
      AppSettings.CalculationApiSendResultUrl,
      httpRequestContent,
      cancellationToken);

    using var responseMessage = await httpRequestTask.ConfigureAwait(false);

    responseMessage.EnsureSuccessStatusCode();
  }
}
