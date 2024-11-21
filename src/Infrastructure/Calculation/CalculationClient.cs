namespace Fibonacci.Infrastructure.Calculation;

/// <summary>
/// Клиент расчёта.
/// </summary>
/// <param name="_calculationService">Сервис расчёта.</param>
public class CalculationClient(ICalculationService _calculationService) : ICalculationClient
{
  /// <inheritdoc/>
  public CalculationResult GetNextCalculationResult(Guid calculationId, CalculationResult previousCalculationResult)
  {
    var calculationResultDTO = previousCalculationResult.ToCalculationResultDTO(calculationId);

    calculationResultDTO = _calculationService.GetNextCalculationResult(calculationResultDTO);

    return calculationResultDTO.ToCalculationResult();
  }
}
