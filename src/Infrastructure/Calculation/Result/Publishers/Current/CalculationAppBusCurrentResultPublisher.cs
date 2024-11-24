namespace Fibonacci.Infrastructure.Calculation.Result.Publishers.Current;

/// <summary>
/// Публикатор текущего результата расчёта в шину приложения.
/// </summary>
/// <param name="_appBus">Шина приложения.</param>
public class CalculationAppBusCurrentResultPublisher(IAppBus _appBus) : ICalculationCurrentResultPublisher
{
  /// <inheritdoc/>
  public Task PublishCalculationResult(
    Guid calculationId,
    CalculationResult calculationResult,
    CancellationToken cancellationToken)
  {
    var calculationResultDTO = calculationResult.ToCalculationResultDTO(calculationId);

    return _appBus.Publish(calculationId.ToString(), calculationResultDTO, cancellationToken);
  }
}
