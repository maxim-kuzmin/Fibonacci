namespace Fibonacci.DomainModel.Calculation;

/// <summary>
/// Результат расчёта.
/// </summary>
/// <param name="Input">Входные данные.</param>
/// <param name="Output">Выходные данные.</param>
public record CalculationResult(BigInteger Input, BigInteger Output);
