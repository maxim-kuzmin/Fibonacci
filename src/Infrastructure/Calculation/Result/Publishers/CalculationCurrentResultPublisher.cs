namespace Fibonacci.Infrastructure.Calculation.Result.Publishers;

/// <summary>
/// Публикатор текущего результата расчёта.
/// </summary>
/// <param name="_appMessageBus">Шина приложения.</param>
public class CalculationCurrentResultPublisher(IAppBus _appMessageBus) : ICalculationCurrentResultPublisher
{
  /// <inheritdoc/>
  public Task PublishCalculationResult(
    Guid calculationId,
    CalculationResult calculationResult,
    CancellationToken cancellationToken)
  {
    var calculationResultDTO = calculationResult.ToCalculationResultDTO(calculationId);

    return _appMessageBus.Publish(calculationId.ToString(), calculationResultDTO, cancellationToken);
  }
}
