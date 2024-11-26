using System.Numerics;

namespace Fibonacci.Apps.FirstApp.Calculation;

/// <summary>
/// Параметры расчёта.
/// </summary>
/// <param name="CalculationCount">Количество расчётов.</param>
/// <param name="MaxInput">Максимальное значение входных данных.</param>
public record CalculationOptions(int CalculationCount, BigInteger MaxInput);
