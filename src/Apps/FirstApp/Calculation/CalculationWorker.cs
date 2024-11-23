namespace Fibonacci.Apps.FirstApp.Calculation;

/// <summary>
/// Исполнитель расчётов.
/// </summary>
/// <param name="_logger">Логгер.</param>
/// <param name="_calculationCount">Количество расчётов.</param>
/// <param name="_calculationClient">Клиент расчёта.</param>
/// <param name="_calculationConsumerFactory">Фабрика подписчиков расчёта.</param>
public class CalculationWorker(
  ILogger<CalculationWorker> _logger,
  CalculationCount _calculationCount,
  ICalculationClient _calculationClient,
  ICalculationResultConsumerFactory _calculationConsumerFactory) : BackgroundService
{
  /// <inheritdoc/>
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    _logger.LogInformation("Calculation work started");

    List<Task> tasks = new(_calculationCount.Value);

    for (var i = 0; i < _calculationCount.Value; i++)
    {
      tasks.Add(Task.Run(() => Calculate(stoppingToken), stoppingToken));
    }

    await Task.WhenAll(tasks);

    _logger.LogInformation("Calculation work finished");
  }

  private async Task Calculate(CancellationToken cancellationToken)
  {
    var calculationId = Guid.NewGuid();

    _logger.LogInformation("Calculation {calculationId} started", calculationId);

    CalculationResult calculationResult = new(0, 0);

    var calculationConsumer = _calculationConsumerFactory.CreateCalculationResultConsumer(calculationId);

    while (!cancellationToken.IsCancellationRequested)
    // //makc// for (var i = 0; i < 10; i++)
    {
      calculationResult = _calculationClient.GetNextCalculationResult(calculationId, calculationResult);

      Display(calculationId, calculationResult);

      calculationResult = await calculationConsumer.GetNextCalculationResult(calculationResult, cancellationToken);

      Display(calculationId, calculationResult);
    }

    _logger.LogInformation("Calculation {calculationId} stopped", calculationId);
  }

  private void Display(Guid calculationId, CalculationResult calculationResult)
  {
    _logger.LogInformation("{calculationId}: {calculationResult}", calculationId, calculationResult);

    Console.WriteLine($"{calculationId}: {calculationResult}");
  }
}
