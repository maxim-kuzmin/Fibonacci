namespace Fibonacci.DomainModel.Test.Calculation.Result;

public class CalculationResultTestData
{
  private static readonly CalculationResult[] _previousCalculationResults =
    [new(0, 0), new(1, 1), new(2, 1), new(3, 2), new(4, 3), new(5, 5)];

  private static readonly CalculationResult[] _nextCalculationResults =
    [new(1, 1), new(2, 1), new(3, 2), new(4, 3), new(5, 5), new(6, 8)];

  public static Guid CalculationId { get; } = Guid.NewGuid();

  public static int CalculationResultCount => _previousCalculationResults.Length;

  public static CalculationResult GetNextCalculationResultByIndex(int index)
  {
    return _nextCalculationResults[index];
  }

  public static CalculationResult GetPreviousCalculationResultByIndex(int index)
  {
    return _previousCalculationResults[index];
  }
}
