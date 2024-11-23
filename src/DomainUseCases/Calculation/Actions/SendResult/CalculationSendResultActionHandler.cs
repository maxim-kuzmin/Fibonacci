namespace Fibonacci.DomainUseCases.Calculation.Actions.SendResult;

/// <summary>
/// Обработчик действия по отправке результата расчёта.
/// </summary>
/// <param name="_calculationClient">Клиент расчёта.</param>
/// <param name="_calculationCurrentResultPublisher">Публикатор текущего результата расчёта.</param>
public class CalculationSendResultActionHandler(
  ICalculationClient _calculationClient,
  ICalculationCurrentResultPublisher _calculationCurrentResultPublisher) :
  IRequestHandler<CalculationSendResultActionCommand>
{
  /// <inheritdoc/>
  public async Task Handle(CalculationSendResultActionCommand request, CancellationToken cancellationToken)
  {
    var сalculationResult = request.ToCalculationResult();

    сalculationResult = _calculationClient.GetNextCalculationResult(
      request.CalculationId,
      сalculationResult);

    var publishTask = _calculationCurrentResultPublisher.PublishCalculationResult(
      request.CalculationId,
      сalculationResult,
      cancellationToken);

    await publishTask.ConfigureAwait(false);
  }
}
