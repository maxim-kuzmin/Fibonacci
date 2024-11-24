
namespace Fibonacci.Apps.FirstApp.Calculation;

public class CalculationMonitor(ILogger<CalculationWorker> _logger) : ICalculationMonitor
{
  public void Display(Guid calculationId, CalculationResult calculationResult)
  {
    _logger.LogInformation("{calculationId}: {calculationResult}", calculationId, calculationResult);

    Console.WriteLine($"{calculationId}: {calculationResult}");
  }
}
