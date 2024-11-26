namespace Fibonacci.Apps.FirstApp.Calculation;

/// <summary>
/// Исполнитель расчётов.
/// </summary>
/// <param name="_logger">Логгер.</param>
/// <param name="_calculationOptions">Параметры расчёта.</param>
/// <param name="_calculationService">Сервис расчёта.</param>
public class CalculationWorker(
  ILogger<CalculationWorker> _logger,
  CalculationOptions _calculationOptions,
  ICalculationService _calculationService) : BackgroundService
{
  /// <inheritdoc/>
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    _logger.LogInformation("Calculation work started");

    List<Task> tasks = new(_calculationOptions.CalculationCount);

    for (var i = 0; i < _calculationOptions.CalculationCount; i++)
    {
      tasks.Add(Task.Run(() => _calculationService.Calculate(stoppingToken), stoppingToken));
    }

    await Task.WhenAll(tasks);

    _logger.LogInformation("Calculation work finished");
  }
}
