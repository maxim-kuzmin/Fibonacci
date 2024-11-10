namespace Fibonacci.Infrastructure.Calculation.Actions.Send;

/// <summary>
/// Обработчик действия по отправке результата расчёта.
/// </summary>
/// <param name="_calculationClient">Клиент расчёта.</param>
/// <param name="_calculationPublisher">Издатель расчёта.</param>
public class CalculationSendResultActionHandler(
  ICalculationClient _calculationClient,
  ICalculationPublisher _calculationPublisher) : ICalculationSendResultActionHandler
{
  /// <inheritdoc/>
  public async Task Handle(CalculationSendResultActionCommand request, CancellationToken cancellationToken)
  {
    var calculationResult = request.ToCalculationResult();

    calculationResult = _calculationClient.GetNextCalculationResult(request.CalculationId, calculationResult);

    var publishTask = _calculationPublisher.PublishNextCalculationResult(request.CalculationId, calculationResult);

    await publishTask.ConfigureAwait(false);
  }
}
