namespace Fibonacci.DomainUseCases.Calculation.DTOs;

/// <summary>
/// Объект передачи данных результата расчёта.
/// Входные и выходные данные передаются в виде строк, чтобы не возникло проблем с сериализацией больших целых чисел.
/// </summary>
/// <param name="CalculationId">Идентификатор расчёта.</param>
/// <param name="Input">Входные данные.</param>
/// <param name="Output">Выходные данные.</param>
public record CalculationResultDTO(Guid CalculationId, string Input, string Output) :
  SerializableCalculationResult(Input, Output);
