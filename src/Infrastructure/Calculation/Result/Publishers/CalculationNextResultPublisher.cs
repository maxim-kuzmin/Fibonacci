namespace Fibonacci.Infrastructure.Calculation.Result.Publishers;

/// <summary>
/// Публикатор следующего результата расчёта.
/// </summary>
/// <param name="_httpClientFactory">Шина приложения.</param>
public class CalculationNextResultPublisher(IHttpClientFactory _httpClientFactory) : ICalculationNextResultPublisher
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
