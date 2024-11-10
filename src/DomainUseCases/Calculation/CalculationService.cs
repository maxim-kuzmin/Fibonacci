namespace Fibonacci.DomainUseCases.Calculation;

/// <summary>
/// Сервис расчёта.
/// </summary>
/// <param name="_calculationLogicServiceFactory">Фабрика сервисов логики расчёта.</param>
public class CalculationService(ICalculationLogicServiceFactory _calculationLogicServiceFactory) : ICalculationService
{
  private readonly ConcurrentDictionary<Guid, ICalculationLogicService> _calculationLogicServiceLookup = new();

  /// <inheritdoc/>
  public CalculationResultDTO GetNextCalculationResult(CalculationResultDTO calculationResultDTO)
  {
    ArgumentNullException.ThrowIfNull(calculationResultDTO);

    var calculationLogicService = _calculationLogicServiceLookup.GetOrAdd(
      calculationResultDTO.CalculationId,
      _ => _calculationLogicServiceFactory.CreateCalculationLogicService());

    var calculationResult = calculationResultDTO.ToCalculationResult();

    calculationResult = calculationLogicService.GetNextCalculationResult(calculationResult);

    return calculationResult.ToCalculationResultDTO(calculationResultDTO.CalculationId);
  }
}
