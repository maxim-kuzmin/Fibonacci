namespace Fibonacci.DomainModel.Calculation.Results;

/// <summary>
/// Сериализуемый результат расчёта.
/// </summary>
/// <param name="Input">Входные данные.</param>
/// <param name="Output">Выходные данные.</param>
public record SerializableCalculationResult(string Input, string Output);
