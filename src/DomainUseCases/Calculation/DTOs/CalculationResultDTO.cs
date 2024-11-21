namespace Fibonacci.DomainUseCases.Calculation.DTOs;

/// <summary>
/// Объект передачи данных результата расчёта.
/// </summary>
/// <param name="CalculationId">Идентификатор расчёта.</param>
/// <param name="CalculationResult">Результат расчёта.</param>
public record CalculationResultDTO(Guid CalculationId, SerializableCalculationResult CalculationResult);
