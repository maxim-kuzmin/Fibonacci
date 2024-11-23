namespace Fibonacci.Infrastructure.Calculation.Result;

/// <summary>
/// Подделка потребителя результата расчёта.
/// Нужна для получения результата расчёта без использования очереди сообщений.
/// </summary>
/// <param name="_calculationId">Идентификатор расчёта.</param>
/// <param name="_calculationService">Сервис расчёта.</param>
public class CalculationResultConsumerFake(Guid _calculationId, ICalculationService _calculationService) :
  ICalculationResultConsumer
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
