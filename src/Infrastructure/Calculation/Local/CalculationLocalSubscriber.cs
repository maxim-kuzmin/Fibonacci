namespace Fibonacci.Infrastructure.Calculation.Local;

/// <summary>
/// Локальный подписчик расчёта. Нужен для получения результата расчёта без использования очереди сообщений.
/// </summary>
/// <param name="_calculationId">Идентификатор расчёта.</param>
/// <param name="_calculationService">Сервис расчёта.</param>
public class CalculationLocalSubscriber(Guid _calculationId, ICalculationService _calculationService) :
  ICalculationSubscriber
{
  private readonly CalculationClient _calculationClient = new(_calculationService);

  /// <inheritdoc/>
  public Task<CalculationResult> GetNextCalculationResult(
    CalculationResult previousCalculationResult,
    CancellationToken cancellationToken)
  { 
    var calculationResult = _calculationClient.GetNextCalculationResult(_calculationId, previousCalculationResult);

    return Task.FromResult(calculationResult);
  }
}
