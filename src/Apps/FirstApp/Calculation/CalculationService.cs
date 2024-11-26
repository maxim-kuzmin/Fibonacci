namespace Fibonacci.Apps.FirstApp.Calculation;

/// <summary>
/// Сервис расчёта.
/// </summary>
/// <param name="_logger">Логгер.</param>
/// <param name="_calculationOptions">Параметры расчёта.</param>
/// <param name="_calculationMonitor">Монитор расчёта.</param>
/// <param name="_calculationClient">Клиент расчёта.</param>
/// <param name="_calculationConsumerFactory">Фабрика подписчиков расчёта.</param>
public class CalculationService(
  ILogger<CalculationWorker> _logger,  
  CalculationOptions _calculationOptions,
  ICalculationMonitor _calculationMonitor,
  ICalculationClient _calculationClient,  
  ICalculationResultConsumerFactory _calculationConsumerFactory) : ICalculationService
{
  /// <inheritdoc/>
  public async Task Calculate(CancellationToken cancellationToken)
  {
    var calculationId = Guid.NewGuid();

    _logger.LogInformation("Calculation {calculationId} started", calculationId);

    CalculationResult calculationResult = new(0, 0);

    var calculationConsumer = _calculationConsumerFactory.CreateCalculationResultConsumer(calculationId);

    while (true)
    {
      if (ShouldBeStopped(calculationResult, cancellationToken))
      {
        break;
      }

      calculationResult = _calculationClient.GetNextCalculationResult(calculationId, calculationResult);

      _calculationMonitor.Display(calculationId, calculationResult);

      if (ShouldBeStopped(calculationResult, cancellationToken))
      {
        break;
      }

      calculationResult = await calculationConsumer.GetNextCalculationResult(calculationResult, cancellationToken);

      _calculationMonitor.Display(calculationId, calculationResult);
    }

    _logger.LogInformation("Calculation {calculationId} stopped", calculationId);
  }

  private bool ShouldBeStopped(CalculationResult calculationResult, CancellationToken cancellationToken)
  {
    return cancellationToken.IsCancellationRequested
      ||
      _calculationOptions.MaxInput > 0 && calculationResult.Input == _calculationOptions.MaxInput;
  }
}
