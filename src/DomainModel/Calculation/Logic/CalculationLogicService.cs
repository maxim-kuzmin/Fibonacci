namespace Fibonacci.DomainModel.Calculation.Logic;

/// <summary>
/// Сервис логики расчёта.
/// </summary>
public class CalculationLogicService : ICalculationLogicService
{
  private BigInteger _output = 0;

  /// <inheritdoc/>
  public CalculationResult GetNextCalculationResult(CalculationResult previousCalculationResult)
  {
    ArgumentOutOfRangeException.ThrowIfNegative(
      previousCalculationResult.Input,
      nameof(previousCalculationResult.Input));

    ArgumentOutOfRangeException.ThrowIfNegative(
      previousCalculationResult.Output,
      nameof(previousCalculationResult.Output));

    _output = _output > 0 ? previousCalculationResult.Output + _output : 1;

    return new(previousCalculationResult.Input + 1, _output);
  }
}
