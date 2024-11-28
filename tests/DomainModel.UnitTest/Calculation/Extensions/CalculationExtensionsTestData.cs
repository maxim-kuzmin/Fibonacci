namespace Fibonacci.DomainModel.Test.Calculation.Extensions;

public class CalculationExtensionsTestData
{
  private static readonly CalculationResult[] _calculationResults =
    [new(0, 0), new(1, 1), new(-1, 1), new(1, -1), new(-1, -1)];

  public static Guid CalculationId { get; } = Guid.NewGuid();

  public static int CalculationResultCount => _calculationResults.Length;

  public static CalculationResult GetCalculationResultByIndex(int index)
  {
    return _calculationResults[index];
  }
}
