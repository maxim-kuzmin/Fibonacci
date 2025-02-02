namespace Fibonacci.DomainUseCases.Calculation.Result.Publishers.Next;

/// <summary>
/// Публикатор следующего результата расчёта в шину приложения.
/// </summary>
/// <param name="_calculationService">Сервис расчёта.</param>
/// <param name="_calculationCurrentResultPublisher">убликатор текущего результата расчёта.</param>
public class CalculationAppBusNextResultPublisher(
  ICalculationService _calculationService,
  ICalculationCurrentResultPublisher _calculationCurrentResultPublisher) : ICalculationNextResultPublisher
{
  private readonly CalculationClient _calculationClient = new(_calculationService);

  /// <inheritdoc/>
  public Task PublishCalculationResult(
    Guid calculationId,
    CalculationResult calculationResult,
    CancellationToken cancellationToken)
  {
    var command = calculationResult.ToCalculationSendResultActionCommand(calculationId);

    var handler = new CalculationSendResultActionHandler(_calculationClient, _calculationCurrentResultPublisher);

    return handler.Handle(command, cancellationToken);
  }
}
