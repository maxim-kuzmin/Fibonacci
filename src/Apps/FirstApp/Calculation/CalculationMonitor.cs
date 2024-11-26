
namespace Fibonacci.Apps.FirstApp.Calculation;

/// <summary>
/// Монитор расчёта.
/// </summary>
/// <param name="_logger">Логгер.</param>
public class CalculationMonitor(ILogger<CalculationWorker> _logger) : ICalculationMonitor
{
  public void Display(Guid calculationId, CalculationResult calculationResult)
  {
    _logger.LogInformation("{calculationId}: {calculationResult}", calculationId, calculationResult);

    Console.WriteLine($"{calculationId}: {calculationResult}");
  }
}
